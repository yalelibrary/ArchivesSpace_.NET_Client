using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class Container
    {
        [JsonProperty("lock_version")]
        public int LockVersion { get; set; }

        [JsonProperty("container_profile_key")]
        public string ContainerProfileKey { get; set; }

        [JsonProperty("type_1")]
        public string Type1 { get; set; }

        [JsonProperty("indicator_1")]
        public string Indicator1 { get; set; }

        [JsonProperty("barcode_1")]
        public string Barcode1 { get; set; }

        [JsonProperty("type_2")]
        public string Type2 { get; set; }

        [JsonProperty("indicator_2")]
        public string Indicator2 { get; set; }

        [JsonProperty("type_3")]
        public string Type3 { get; set; }

        [JsonProperty("indicator_3")]
        public string Indicator3 { get; set; }

        [JsonProperty("container_extent")]
        public string ContainerExtent { get; set; }

        [JsonProperty("container_extent_type")]
        public string ContainerExtentType { get; set; }

        [JsonProperty("container_locations")]
        public ICollection<ContainerLocation> ContainerLocations { get; set; }
    }
}