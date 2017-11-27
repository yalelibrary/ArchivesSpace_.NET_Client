using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class SearchResultArchivalObject : SearchResultBase
    {
        [JsonProperty("results")]
        public ICollection<SearchResultEntryArchivalObject> Results { get; set; }
    }
}