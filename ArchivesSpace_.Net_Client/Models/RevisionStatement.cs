using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class RevisionStatement
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}