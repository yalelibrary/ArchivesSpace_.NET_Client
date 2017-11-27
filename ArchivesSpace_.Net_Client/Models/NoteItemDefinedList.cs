using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class NoteItemDefinedList : NoteItemBase
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("items")]
        public ICollection<NoteLabelValuePair> Items { get; set; }

        public class NoteLabelValuePair
        {
            [JsonProperty("label")]
            public string Label { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }
    }
}