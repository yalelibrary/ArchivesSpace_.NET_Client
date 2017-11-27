using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class LocationProfile
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display_string")]
        public string DisplayString { get; set; }

        [JsonProperty("dimension_units")]
        public string DimensionUnits { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("width")]
        public string Width { get; set; }

        [JsonProperty("depth")]
        public string Depth { get; set; }
    }
}