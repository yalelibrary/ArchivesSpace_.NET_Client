using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchivesSpace_.Net_Client.Models;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client
{
    public class ArchivesSpaceContainerProfileManager : ArchivesSpaceObjectManagerBase
    {
        public ArchivesSpaceContainerProfileManager(ArchivesSpaceService activeService)
            : base(activeService)
        {

        }

        public virtual async Task<List<int>> GetAllContainerProfileIdsAsync()
        {
            return await GetAllContainerProfileIdsActionAsync();
        }

        public virtual async Task<ContainerProfile> GetContainerProfileByIdAsync(int id)
        {
            return await GetContainerProfileByIdActionAsync(id);
        }

        public virtual async Task<ICollection<ContainerProfile>> GetContainerProfileByIdsAsync(ICollection<int> idCollection)
        {
            return await GetContainerProfileByIdsActionAsync(idCollection);
        }

        public virtual async Task<List<int>> GetAllContainerProfileIdsActionAsync()
        {
            var uriSegment = "/container_profiles?all_ids=true";
            var allCpsString = await ArchivesSpaceService.GetApiDataNoRepositoryAsync(uriSegment);
            var alCps = JsonConvert.DeserializeObject<List<int>>(allCpsString);
            return alCps;
        }

        public virtual async Task<ContainerProfile> GetContainerProfileByIdActionAsync(int id)
        {
            var uriSegment = String.Format("/container_profiles/{0}", id);
            var locString = await ArchivesSpaceService.GetApiDataNoRepositoryAsync(uriSegment);
            var loc = JsonConvert.DeserializeObject<ContainerProfile>(locString);
            return loc;
        }

        public virtual async Task<ICollection<ContainerProfile>> GetContainerProfileByIdsActionAsync(ICollection<int> idCollection)
        {
            idCollection = idCollection.Distinct().ToList();
            var containerBag = new ConcurrentBag<ContainerProfile>();
            await idCollection.ForEachAsync(Environment.ProcessorCount, async x =>
            {
                containerBag.Add(await GetContainerProfileByIdActionAsync(x));
            });
            return containerBag.ToList();
        }


    }
}