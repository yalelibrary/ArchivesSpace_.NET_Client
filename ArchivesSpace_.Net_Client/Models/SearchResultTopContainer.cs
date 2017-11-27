using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class SearchResultTopContainer : SearchResultBase
    {
        [JsonProperty("results")]
        public ICollection<SearchResultEntryTopContainer> Results { get; set; }
    }
}