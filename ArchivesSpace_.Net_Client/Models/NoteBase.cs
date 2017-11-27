using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class NoteBase
    {
        [JsonProperty("jsonmodel_type")]
        public string JsonModelType { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("publish")]
        public bool Publish { get; set; }

        [JsonProperty("persistent_id")]
        public string PersistentId { get; set; }

        [JsonProperty("ingest_problem")]
        public string IngestProblem { get; set; }
    }
}
