using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchivesSpace_.Net_Client.Models;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client
{
    public class ArchivesSpaceResourceManager : ArchivesSpaceObjectManagerBase
    {
        public ArchivesSpaceResourceManager(ArchivesSpaceService activeService)
            : base(activeService)
        {
            //This sets the ArchivesSpaceService field in the base class
        }

        public virtual async Task<List<int>> GetAllResourceIdsAsync()
        {
            var uriSegment = "/resources?all_ids=true";
            var allResourcesString = await ArchivesSpaceService.GetApiDataAsync(uriSegment);
            var allResources = JsonConvert.DeserializeObject<List<int>>(allResourcesString);
            return allResources;
        }

        public virtual async Task<Resource> GetResourceByIdAsync(int id)
        {
            var uriSegment = String.Format("/resources/{0}", id);
            var resourceString = await ArchivesSpaceService.GetApiDataAsync(uriSegment);
            var resource = JsonConvert.DeserializeObject<Resource>(resourceString, new JsonAspaceNoteConverter(), new JsonAspaceNoteItemConverter());
            return resource;
        }

        public virtual async Task<RecordTreeResource> GetRecordTreeAsync(int resourceId)
        {
            return await GetRecordTreeActionAsync(resourceId, false);
        }

        public virtual async Task<RecordTreeResource> GetRecordTreeRootLevelAsync(int resourceId)
        {
            return await GetRecordTreeActionAsync(resourceId, true);
        }

        public virtual async Task<ICollection<ArchivalObject>> GetTopLevelSeriesArchivalObjects(int resourceId)
        {
            var archivalObjectCollection = new List<ArchivalObject>();
            var recordTree = await GetRecordTreeActionAsync(resourceId, true);
            if (recordTree.Children.Count < 1)
            {
                return archivalObjectCollection;
            }
            var archivalObjectManager = new ArchivesSpaceArchivalObjectManager(this.ArchivesSpaceService);
            var lookupList = new List<Task<ArchivalObject>>();
            foreach (var archivalObject in recordTree.Children)
            {
                lookupList.Add(archivalObjectManager.GetArchivalObjectByIdAsync(archivalObject.Id));
            }
            await Task.WhenAll(lookupList);
            foreach (var task in lookupList)
            {
                archivalObjectCollection.Add(task.Result);
            }
            return archivalObjectCollection;
        }

        public virtual async Task<SmallTree> GetSmallResourceTreeAsync(int resourceId)
        {
            return await GetSmallResourceTreeActionAsync(resourceId);
        }

        public virtual async Task<ICollection<TopContainer>> GetAllTopContainersForResourceAsync(Resource resource)
        {
            return await GetAllTopContainersForResourceActionAsync(resource);
        }

        public virtual async Task<ICollection<TopContainer>> GetAllTopContainersForResourceAsync(int resourceId)
        {
            return await GetAllTopContainersForResourceActionAsync(resourceId);
        }

        private async Task<ICollection<TopContainer>> GetAllTopContainersForResourceActionAsync(int id)
        {
            //long running - fetches all containers for resource
            var resource = await GetResourceByIdAsync(id);
            return await GetAllTopContainersForResourceActionAsync(resource);

        }

        private async Task<ICollection<TopContainer>> GetAllTopContainersForResourceActionAsync(Resource resource)
        {
            var resultList = new List<TopContainer>();
            var topContainerManager = new ArchivesSpaceTopContainerManager(ArchivesSpaceService);

            var initialSearchResult = await topContainerManager.GetTopContainerResourceSearchAsync(resource.Uri, true);
            foreach (var searchResult in initialSearchResult.Results)
            {
                resultList.Add(searchResult.ParsedJson);
            }
            return resultList;
        }

        private async Task<SmallTree> GetSmallResourceTreeActionAsync(int resourceId)
        {
            var uriSegment = String.Format("/resources/{0}/small_tree", resourceId);
            var recordTreeString = await ArchivesSpaceService.GetApiDataAsync(uriSegment);
            var recordTree = JsonConvert.DeserializeObject<SmallTree>(recordTreeString);
            return recordTree;
        }

        private async Task<RecordTreeResource> GetRecordTreeActionAsync(int resourceId, bool firstLevelOnly)
        {
            var limitParameter = firstLevelOnly ? "?limit_to=root" : "";
            var uriSegment = String.Format("/resources/{0}/tree{1}", resourceId, limitParameter);
            var recordTreeString = await ArchivesSpaceService.GetApiDataAsync(uriSegment);
            var recordTree = JsonConvert.DeserializeObject<RecordTreeResource>(recordTreeString);
            return recordTree;
        }
    }
}