using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class SearchResultBase
    {
        [JsonProperty("page_size")]
        public int PageSize { get; set; }

        [JsonProperty("first_page")]
        public int FirstPage { get; set; }

        [JsonProperty("last_page")]
        public int LastPage { get; set; }

        [JsonProperty("this_page")]
        public int ThisPage { get; set; }

        [JsonProperty("offset_first")]
        public int OffsetFirst { get; set; }

        [JsonProperty("offset_last")]
        public int OffsetLast { get; set; }

        [JsonProperty("total_hits")]
        public int TotalHits { get; set; }
    }
}