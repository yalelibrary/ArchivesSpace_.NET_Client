using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class SearchResultEntryArchivalObject : SearchResultEntryBase
    {
        private ArchivalObject _deserializedJson;
        
        [JsonIgnore]
        public virtual ArchivalObject ParsedJson
        {
            get
            {
                if (_deserializedJson == null)
                {
                    _deserializedJson = JsonConvert.DeserializeObject<ArchivalObject>(Json, new JsonAspaceNoteConverter(), new JsonAspaceNoteItemConverter());
                }
                return _deserializedJson;
            }
        }
        [JsonProperty("type_enum_s")]
        public ICollection<string> TypeEnumS { get; set; }

        [JsonProperty("type_1_enum_s")]
        public ICollection<string> Type1EnumS { get; set; }

        [JsonProperty("type_2_enum_s")]
        public ICollection<string> Type2EnumS { get; set; }
        
        [JsonProperty("level_enum_s")]
        public ICollection<string> LevelEnumS { get; set; }

        [JsonProperty("extent_type_enum_s")]
        public ICollection<string> ExtentTypeEnumS { get; set; }

        [JsonProperty("instance_type_enum_s")]
        public ICollection<string> InstanceTypeEnumS { get; set; }

        [JsonProperty("language_enum_s")]
        public ICollection<string> LanguageEnumS { get; set; }

        [JsonProperty("date_type_enum_s")]
        public ICollection<string> DateTypeEnumS { get; set; }

        [JsonProperty("label_enum_s")]
        public ICollection<string> LabelEnumS { get; set; }

        [JsonProperty("portion_enum_s")]
        public ICollection<string> PortionEnumS { get; set; }

        [JsonProperty("use_statement_enum_s")]
        public ICollection<string> UseStatementEnumS { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("top_container_uri_u_sstr")]
        public ICollection<string> TopContainerUriUSstr { get; set; }

        [JsonProperty("external_id")]
        public ICollection<string> ExternalId { get; set; }

    }

}