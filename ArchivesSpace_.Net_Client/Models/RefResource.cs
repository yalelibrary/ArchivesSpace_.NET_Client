using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class RefResource : RefBase
    {
        [JsonProperty("identifier")] //appears with top container ref
        public string Identifier { get; set; }

        [JsonProperty("display_string")] //appears with top container ref
        public string DisplayString { get; set; }
    }
}