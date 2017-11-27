using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class NoteSinglepart : NoteBase
    {
        [JsonProperty("content")]
        public ICollection<string> Content { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}