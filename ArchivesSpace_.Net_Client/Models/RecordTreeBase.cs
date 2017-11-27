using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class RecordTreeBase
    {
        [JsonProperty("jsonmodel_type")]
        public string JsonModelType { get; set; }

        [JsonProperty("lock_version")]
        public int LockVersion {get; set; }

        [JsonProperty("uri")]
        public string Uri {get;set;}

        [JsonProperty("id")]
        public int Id {get;set;}

        [JsonProperty("record_uri")]
        public string RecordUri {get;set;}

        [JsonProperty("title")]
        public string Title {get;set;}

        [JsonProperty("suppressed")]
        public bool Suppressed {get;set;}

        [JsonProperty("Publish")]
        public bool Publish {get;set;}

        [JsonProperty("has_children")]
        public bool HasChildren {get;set;}

        [JsonProperty("node_type")]
        public string NoteType {get;set;}
    }
}