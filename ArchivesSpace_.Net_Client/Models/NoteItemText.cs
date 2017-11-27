using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class NoteItemText : NoteItemBase
    {
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}