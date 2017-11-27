using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArchivesSpace_.Net_Client.Models;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client
{
    public class ArchivesSpaceAccessionManager : ArchivesSpaceObjectManagerBase
    {

        public ArchivesSpaceAccessionManager(ArchivesSpaceService activeService) : base(activeService)
        {
            //This sets the ArchivesSpaceService field in the base class
        }

        public async Task<List<int>> GetAllAccessionIdsAsync()
        {
            var uriSegment = "/accessions?all_ids=true";
            var allAccessionsString = await ArchivesSpaceService.GetApiDataAsync(uriSegment);
            var allAccessions = JsonConvert.DeserializeObject<List<int>>(allAccessionsString);
            return allAccessions;
        }

        public async Task<Accession> GetAccessionByIdAsync(int id)
        {
            var uriSegment = String.Format("/accessions/{0}", id);
            var accessionString = await ArchivesSpaceService.GetApiDataAsync(uriSegment);
            var accession = JsonConvert.DeserializeObject<Accession>(accessionString);
            return accession;
        }

    }
}