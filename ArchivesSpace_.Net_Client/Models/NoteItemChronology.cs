using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class NoteItemChronology : NoteItemBase
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("items")]
        public ICollection<EventItem> Items { get; set; }
        
        public class EventItem
        {
            [JsonProperty("event_date")]
            public string EventDate { get; set; }

            [JsonProperty("events")]
            public ICollection<string> Events { get; set; }
        }
    }
}