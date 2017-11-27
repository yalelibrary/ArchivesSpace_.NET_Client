using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class Deaccession
    {
        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("disposition")]
        public string Disposition { get; set; }

        [JsonProperty("notification")]
        public bool Notification { get; set; }

        [JsonProperty("date")]
        public Date Date { get; set; }

        [JsonProperty("extents")]
        public ICollection<Extent> Extents { get; set; }
    }
}