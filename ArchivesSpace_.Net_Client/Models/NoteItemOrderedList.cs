using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class NoteItemOrderedList : NoteItemBase
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("enumeration")]
        public string Enumeration { get; set; }

        [JsonProperty("items")]
        public ICollection<string> Items { get; set; }
    }
}