using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchivesSpace_.Net_Client.Models;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client
{
    public class ArchivesSpaceLocationManager : ArchivesSpaceObjectManagerBase
    {
        public ArchivesSpaceLocationManager(ArchivesSpaceService activeService)
            : base(activeService)
        {
            
        }

        public virtual async Task<List<int>> GetAllLocationIdsAsync()
        {
            return await GetAllLocationIdsActionAsync();
        }

        public virtual async Task<Location> GetLocationByIdAsync(int id)
        {
            return await GetLocationByIdActionAsync(id);
        }

        public virtual async Task<ICollection<Location>> GetLocationsByIdsAsync(ICollection<int> idCollection)
        {
            return await GetLocationsByIdsActionAsync(idCollection);
        }

        private async Task<List<int>> GetAllLocationIdsActionAsync()
        {
            var uriSegment = "/locations?all_ids=true";
            var allTcsString = await ArchivesSpaceService.GetApiDataNoRepositoryAsync(uriSegment);
            var alTcs = JsonConvert.DeserializeObject<List<int>>(allTcsString);
            return alTcs;
        }

        private async Task<Location> GetLocationByIdActionAsync(int id)
        {
            var uriSegment = String.Format("/locations/{0}", id);
            var locString = await ArchivesSpaceService.GetApiDataNoRepositoryAsync(uriSegment);
            var loc = JsonConvert.DeserializeObject<Location>(locString, new JsonAspaceNoteConverter(), new JsonAspaceNoteItemConverter());
            return loc;
        }

        private async Task<ICollection<Location>> GetLocationsByIdsActionAsync(ICollection<int> idCollection)
        {
            idCollection = idCollection.Distinct().ToList();
            var containerBag = new ConcurrentBag<Location>();
            //reading only so I'm not worrying about the source object being a regular collection rather than concurrent collection
            await idCollection.ForEachAsync(Environment.ProcessorCount, async x =>
            {
                containerBag.Add(await GetLocationByIdActionAsync(x));
            });
            return containerBag.ToList();
        }


    }
}