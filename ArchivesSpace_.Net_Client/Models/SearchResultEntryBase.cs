using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class SearchResultEntryBase
    {
        [JsonIgnore]
        public virtual int IdStripped
        {
            get { return int.Parse(Uri.Split('/').Last()); }
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
        
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("primary_type")]
        public string PrimaryType { get; set; }

        [JsonProperty("jsonmodel_type")]
        public string JsonmodelType { get; set; }

        [JsonProperty("types")]
        public ICollection<string> Types { get; set; }

        [JsonProperty("json")]
        public string Json { get; set; }

        [JsonProperty("suppressed")]
        public bool Suppressed { get; set; }

        [JsonProperty("publish")]
        public bool Publish { get; set; }

        [JsonProperty("system_generated")]
        public bool SystemGenerated { get; set; }

        [JsonProperty("repository")]
        public string Repository { get; set; }

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("last_modified_by")]
        public string LastModifiedBy { get; set; }

        [JsonProperty("user_mtime")]
        public DateTimeOffset UserMtime { get; set; }

        [JsonProperty("system_mtime")]
        public DateTimeOffset SystemMtime { get; set; }

        [JsonProperty("create_time")]
        public DateTimeOffset CreateTime { get; set; }

        [JsonProperty("source_enum_s")]
        public ICollection<string> SourceEnumS { get; set; }
    }
}