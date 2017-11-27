using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ArchivesSpace_.Net_Client.Models;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client
{
    public class ArchivesSpaceArchivalObjectManager : ArchivesSpaceObjectManagerBase
    {
        public ArchivesSpaceArchivalObjectManager(ArchivesSpaceService activeService)
            : base(activeService)
        {
            //This sets the ArchivesSpaceService field in the base class
        }

        public virtual async Task<List<int>> GetAllArchivalObjectIdsAsync()
        {
            var uriSegment = "/archival_objects?all_ids=true";
            var allArchivalObjectsString = await ArchivesSpaceService.GetApiDataAsync(uriSegment);
            var allArchivalObjects = JsonConvert.DeserializeObject<List<int>>(allArchivalObjectsString);
            return allArchivalObjects;
        }

        public virtual async Task<ArchivalObject> GetArchivalObjectByIdAsync(int id)
        {
            return await GetArchivalObjectByIdActionAsync(id);
        }
        
        public virtual async Task<ICollection<ArchivalObject>> GetArchivalObjectChildrenAsync(int id)
        {
            return await GetArchivalObjectChildrenActionAsync(id);
        }

        public virtual async Task<ICollection<TopContainer>> GetAllTopContainersForIdAsync(int id)
        {
            return await GetAllTopContainersForIdActionAsync(id);
        }
        
        public async Task<ICollection<ContainerConsolidated>> GetAllContainersForIdAsync(int id)
        {
            return await GetAllContainersForIdActionAsync(id);
        }

        //This takes much longer to process since it searches for *all* instances which may repeat many times
        public async Task<ICollection<Instance>> GetAllInstancesForIdAsync(int id, bool includeDigital = false)
        {
            return await GetAllInstancesForIdActionAsync(id, includeDigital);
        }

        private async Task<ICollection<TopContainer>> GetAllTopContainersForIdActionAsync(int id)
        {
            //need to figure out whether we're asking at the series level or below
            //if at the series level the component_id field doesn't exist for inventory collections but
            //seems to be required for retrieval in the top container search endpoint so we need to check for it
            var targetId = await GetArchivalObjectByIdActionAsync(id);
            if (targetId.Level == "series" && !(String.IsNullOrEmpty(targetId.ComponentId)))
            {
                return await GetTopContainersFromSeriesWithIdentifierAsync(targetId);
            }
            return await GetTopContainersForArchivalObjectAsync(targetId);

        }

        private async Task<ICollection<TopContainer>>  GetTopContainersFromSeriesWithIdentifierAsync(ArchivalObject seriesArchivalObject)
        {
            var resultList = new List<TopContainer>();
            var topContainerManager = new ArchivesSpaceTopContainerManager(ArchivesSpaceService);

            var initialSearchResult = await topContainerManager.GetTopContainerSeriesSearchAsync(seriesArchivalObject.Uri, true);
            foreach (var searchResult in initialSearchResult.Results)
            {
                resultList.Add(searchResult.ParsedJson);
            }
            return resultList;
        }

        private async Task<ICollection<TopContainer>> GetTopContainersForArchivalObjectAsync(ArchivalObject ao)
        {
            var topContainerManager = new ArchivesSpaceTopContainerManager(ArchivesSpaceService);
            var instances = await GetAllInstancesForIdActionAsync(ao.Id);

            var topContinerIdList =
                instances.Where(x => x.SubContainer.TopContainer.Ref != null)
                    .Select(x => x.SubContainer.TopContainer.RefStrippedId).ToList();

            var topContainerList = await topContainerManager.GetTopContainersByIdsAsync(topContinerIdList);

            return topContainerList;
        }

        private async Task<ICollection<ContainerConsolidated>> GetAllContainersForIdActionAsync(int id)
        {
            var unconsolidatedInstanceList = new ConcurrentBag<Instance>();
            var idsOfDescendantArchivalObjectsWithContainers = await GetIdsOfAllArchivalObjectDescendantsWithPhysicalContainersAsync(id);
            await GetInstancesFromArchivalObjectIdListAsync(idsOfDescendantArchivalObjectsWithContainers, unconsolidatedInstanceList);
            var consolidatedInstanceList = ConsolidateInstances(unconsolidatedInstanceList.ToList());
            return consolidatedInstanceList;
        }

        private async Task<ICollection<Instance>> GetAllInstancesForIdActionAsync(int id, bool includeDigital = false)
        {
            var instanceList = new ConcurrentBag<Instance>();
            var startingArchivalObject = GetArchivalObjectByIdActionAsync(id);
            var startingArchivalObjectChildren = await GetArchivalObjectChildrenActionAsync(id);
            AddInstancesToBag(await startingArchivalObject, instanceList, includeDigital);
            if (startingArchivalObjectChildren.Count > 0)
            {
                await GetInstancesFromArchivalObjectChildrenParallelAsync(startingArchivalObjectChildren, instanceList);
            }
            return instanceList.ToList();
        }

        private ICollection<ContainerConsolidated> ConsolidateInstances(ICollection<Instance> unconsolidatedInstanceCollection)
        {
            var consolidatedContainerCollection = new Dictionary<string, ContainerConsolidated>();
            foreach (var instance in unconsolidatedInstanceCollection)
            {
                var instanceIdentifier = (String.Join("|",instance.Container.Indicator1,instance.Container.Indicator2,instance.Container.Indicator3));
                if (consolidatedContainerCollection.ContainsKey(instanceIdentifier))
                {
                    consolidatedContainerCollection[instanceIdentifier].SubContainers.Add(instance.SubContainer);
                }
                else
                {
                    var consolidatedContainer = new ContainerConsolidated
                    {
                        Barcode1 = instance.Container.Barcode1
                        , ContainerExtent = instance.Container.ContainerExtent
                        , ContainerExtentType = instance.Container.ContainerExtentType
                        , ContainerLocations = instance.Container.ContainerLocations
                        , ContainerProfileKey = instance.Container.ContainerProfileKey
                        , Indicator1 = instance.Container.Indicator1
                        , Indicator2 = instance.Container.Indicator2
                        , Indicator3 = instance.Container.Indicator3
                        , Type1 = instance.Container.Type1
                        , Type2 = instance.Container.Type2
                        , Type3 = instance.Container.Type3
                        , SubContainers = new List<SubContainer>()
                    };
                    consolidatedContainer.SubContainers.Add(instance.SubContainer);
                    consolidatedContainerCollection[instanceIdentifier] = consolidatedContainer;                    
                }
            }
            return consolidatedContainerCollection.Values;
        }

        /*It looksl like it may not be appropriate to use parallel on top of async processing on a webserver but it's a huge performance increase for this task
         *so it's worth revisiting if there are issues discovered later
         *Also, it turns out that "file" type archival objects can both have children and also have instances attached directly, see AO with ID 810a253b15ac77395140a6b803a1f0ae
         * (https://archivesspace.library.yale.edu/resources/2110#tree::archival_object_785223 in MSSA) for an example */
        private async Task GetInstancesFromArchivalObjectChildrenParallelAsync(ICollection<ArchivalObject> archivalObjectCollection, ConcurrentBag<Instance> instanceList, bool includeDigital = false)
        {
            await archivalObjectCollection.ForEachAsync(4, async x =>
            {
                AddInstancesToBag(x, instanceList, includeDigital);
                var childList = await GetArchivalObjectChildrenActionAsync(x.Id);
                if (childList.Count > 0)
                {
                    await GetInstancesFromArchivalObjectChildrenParallelAsync(childList, instanceList);
                }
            });
        }

        //Leaving this temporarily for reference while application is tested in case we need to revert due to error handling issues (or something else)
        private async Task GetInstancesFromArchivalObjectChildrenAsync(ICollection<ArchivalObject> archivalObjectCollection,
            ConcurrentBag<Instance> instanceList, bool includeDigital = false)
        {
            foreach (var archivalObject in archivalObjectCollection)
            {
                AddInstancesToBag(archivalObject, instanceList);
                var childList = await GetArchivalObjectChildrenActionAsync(archivalObject.Id);
                if (childList.Count > 0)
                {
                    await GetInstancesFromArchivalObjectChildrenAsync(childList, instanceList);
                }
            }
        }

        private async Task GetInstancesFromArchivalObjectIdListAsync(ICollection<int> archivalObjectIdList, ConcurrentBag<Instance> instanceList, bool includeDigital = false)
        {
            await archivalObjectIdList.ForEachAsync(Environment.ProcessorCount, async x =>
            {
                AddInstancesToBag(await GetArchivalObjectByIdActionAsync(x), instanceList, includeDigital);
            });
        }

        private async Task<ICollection<ArchivalObject>> GetArchivalObjectChildrenActionAsync(int id)
        {
            var uriSegment = String.Format("/archival_objects/{0}/children", id);
            var archivalObjectCollectionString = await ArchivesSpaceService.GetApiDataAsync(uriSegment);
            var archivalObjectCollection = JsonConvert.DeserializeObject<List<ArchivalObject>>(archivalObjectCollectionString, new JsonAspaceNoteConverter(), new JsonAspaceNoteItemConverter());
            return archivalObjectCollection;
        }

        private async Task<ICollection<int>> GetIdsOfAllArchivalObjectDescendantsWithPhysicalContainersAsync(int archivalObjectId)
        {
            var startingArchivalObject = await GetArchivalObjectByIdActionAsync(archivalObjectId);

            return await GetIdsOfAllArchivalObjectDescendantsWithPhysicalContainersAsync(archivalObjectId,
                startingArchivalObject.Resource.RefStrippedId);

        }

        private async Task<ICollection<int>> GetIdsOfAllArchivalObjectDescendantsWithPhysicalContainersAsync(int archivalObjectId, int resourceId)
        {
            var objectsWithContainers = await GetSmallTreeItemsWithContainersAtId(archivalObjectId, resourceId);
            var listOfIds = objectsWithContainers.Select(x => x.Id).ToList();
            return listOfIds;
        }

        private async Task<ICollection<SmallTree>> GetSmallTreeItemsWithContainersAtId(int archivalObjectId,
            int resourceId)
        {
            var targetArchivalObjectSmallTree = await GetSmallTreeBasedAtId(archivalObjectId, resourceId);
            var objectsWithContainers = targetArchivalObjectSmallTree.Descendants().Where(x => x.Container != null);
            return objectsWithContainers.ToList();
        }

        private async Task<SmallTree> GetSmallTreeBasedAtId(int archivalObjectId, int resourceId)
        {
            var resourceManager = new ArchivesSpaceResourceManager(ArchivesSpaceService);
            var smallTree = await resourceManager.GetSmallResourceTreeAsync(resourceId);
            var targetArchivalObjectSmallTree = smallTree.Descendants().First(x => x.Id == archivalObjectId);
            return targetArchivalObjectSmallTree;
        }

        private void AddInstancesToBag(ArchivalObject archivalObject, ConcurrentBag<Instance> instanceBag, bool includeDigitalObjects = false)
        {
            if (archivalObject.Instances == null || archivalObject.Instances.Count <= 0) return;
            foreach (var instance in archivalObject.Instances)
            {
                if (instance.InstanceType != "digital_object")
                {
                    instanceBag.Add(instance);
                }
                else if (includeDigitalObjects)
                {
                    instanceBag.Add(instance);
                }
            }
        }

        private async Task<ArchivalObject> GetArchivalObjectByIdActionAsync(int id)
        {
            var uriSegment = String.Format("/archival_objects/{0}", id);
            var archivalObjectString = await ArchivesSpaceService.GetApiDataAsync(uriSegment);
            var archivalObject = JsonConvert.DeserializeObject<ArchivalObject>(archivalObjectString, new JsonAspaceNoteConverter(), new JsonAspaceNoteItemConverter());
            return archivalObject;
        }

    }
}