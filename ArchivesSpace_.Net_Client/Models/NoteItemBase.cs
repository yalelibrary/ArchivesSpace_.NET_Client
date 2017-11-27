using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class NoteItemBase
    {
        [JsonProperty("jsonmodel_type")]
        public string JsonModelType { get; set; }

        [JsonProperty("publish")]
        public bool Publish { get; set; }
    }
}