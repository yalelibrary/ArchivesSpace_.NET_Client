using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class SearchResultResource : SearchResultBase
    {
        [JsonProperty("results")]
        public ICollection<SearchResultEntryResource> Results { get; set; }
    }
}