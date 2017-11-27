using System.Dynamic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class SubContainer
    {
        [JsonProperty("lock_version")]
        public int LockVersion { get; set; }

        [JsonProperty("jsonmodel_type")]
        public string JsonmodelType { get; set; }

        [JsonProperty("top_container")]
        public RefTopContainer TopContainer { get; set; } //ref

        [JsonProperty("type_2")]
        public string Type2 { get; set; }

        [JsonProperty("indicator_2")]
        public string Indicator2 { get; set; }

        [JsonProperty("type_3")]
        public string Type3 { get; set; }

        [JsonProperty("indicator_3")]
        public string Indicator3 { get; set; }

        [JsonProperty("display_string")]
        public string DisplayString { get; set; }
    }
}