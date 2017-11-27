using System;
using System.Dynamic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ArchivesSpace_.Net_Client.Models
{
    public class Repository
    {
        //This exists for convenience - it makes working with collections much simpler when the identifier can be accessed directly
        [JsonIgnore]
        public virtual int Id
        {
            get { return int.Parse(Uri.Split('/').Last()); }
            set { Uri = String.Format("/repositories/{0}", value); }
        }

        [JsonProperty("lock_version")]
        public virtual int LockVersion { get; set; }

        [JsonProperty("repo_code")]
        public virtual string RepoCode { get; set; }

        [JsonProperty("name")]
        public virtual string Name { get; set; }

        [JsonProperty("parent_institution_name")]
        public virtual string ParentInstitutionName { get; set; }

        [JsonProperty("url")]
        public virtual string Url { get; set; }

        [JsonProperty("created_by")]
        public virtual string CreatedBy { get; set; }

        [JsonProperty("last_modified_by")]
        public virtual string LastModifiedBy { get; set; }

        [JsonProperty("create_time")]
        public virtual DateTimeOffset CreateTime { get; set; }

        [JsonProperty("system_mtime")]
        public virtual DateTimeOffset SystemModifiedTime { get; set; }

        [JsonProperty("user_mtime")]
        public virtual DateTimeOffset UserModifiedTime { get; set; }

        [JsonProperty("jsonmodel_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ModelTypes JsonmodelType { get; set; }

        [JsonProperty("uri")]
        public virtual string Uri { get; set; }

        [JsonProperty("display_string")]
        public virtual string DisplayString { get; set; }

        [JsonProperty("agent_representation")]
        public virtual dynamic AgentRepresentation { get; set; }
    }
}
