using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class ExternalDocument
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("publish")]
        public bool Publish { get; set; }
    }
}