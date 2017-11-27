using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public abstract class ArchivalObjectBase
    {
        //This exists for convenience - it makes working with collections much simpler when the identifier can be accessed directly
        [JsonIgnore]
        public virtual int Id
        {
            get { return int.Parse(Uri.Split('/').Last()); }
        }

        [JsonProperty("lock_version")]
        public int LockVersion { get; set; }

        [JsonProperty("jsonmodel_type")]
        public string JsonmodelType { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
        
        [JsonProperty("external_ids")]
        public ICollection<ExternalId> ExternalIds { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("publish")]
        public bool Publish { get; set; }

        [JsonProperty("subjects")]
        public ICollection<dynamic> Subjects { get; set; } //ref

        [JsonProperty("linked_events")]
        public ICollection<dynamic> LinkedEvents { get; set; } //ref

        [JsonProperty("extents")]
        public virtual ICollection<Extent> Extents { get; set; }

        [JsonProperty("dates")]
        public virtual ICollection<Date> Dates { get; set; }

        [JsonProperty("external_documents")]
        public ICollection<ExternalDocument> ExternalDocuments { get; set; }

        [JsonProperty("rights_statements")]
        public ICollection<RightsStatement> RightsStatements { get; set; }

        [JsonProperty("linked_agents")]
        public ICollection<dynamic> LinkedAgents { get; set; } //ref

        [JsonProperty("suppressed")]
        public bool Suppressed { get; set; }
    }
}