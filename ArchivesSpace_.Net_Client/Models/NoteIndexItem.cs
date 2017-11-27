using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class NoteIndexItem
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("reference_text")]
        public string ReferenceText { get; set; }

        [JsonProperty("reference_ref")]
        public dynamic ReferenceRef { get; set; } //ref
    }
}