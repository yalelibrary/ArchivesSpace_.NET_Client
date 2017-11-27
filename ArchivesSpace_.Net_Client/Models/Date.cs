using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class Date
    {
        [JsonProperty("date_type")]
        public string DateType { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("certainty")]
        public string Certainty { get; set; }

        [JsonProperty("expression")]
        public string Expression { get; set; }

        [JsonProperty("begin")]
        public string Begin { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("era")]
        public string Era { get; set; }

        [JsonProperty("calendar")]
        public string Calendar { get; set; }
    }
}