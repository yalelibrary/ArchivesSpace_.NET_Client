using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class SearchResultEntryResource : SearchResultEntryBase
    {
        private Resource _deserializedJson;
        
        [JsonIgnore]
        public virtual Resource ParsedJson
        {
            get
            {
                if (_deserializedJson == null)
                {
                    _deserializedJson = JsonConvert.DeserializeObject<Resource>(Json);
                }
                return _deserializedJson;
            }
        }
        [JsonProperty("type_enum_s")]
        public ICollection<string> TypeEnumS { get; set; }

        [JsonProperty("role_enum_s")]
        public ICollection<string> RoleEnumS { get; set; }
        
        [JsonProperty("rules_enum_s")]
        public ICollection<string> RulesEnumS { get; set; }

        [JsonProperty("level_enum_s")]
        public ICollection<string> LevelEnumS { get; set; }

        [JsonProperty("extent_type_enum_s")]
        public ICollection<string> ExtentTypeEnumS { get; set; }

        [JsonProperty("finding_aid_description_rules_enum_s")]
        public ICollection<string> FindingAidDescriptionRulesEnumS { get; set; }

        [JsonProperty("finding_aid_status_enum_s")]
        public ICollection<string> FindingAidStatusEnumS { get; set; }

        [JsonProperty("language_enum_s")]
        public ICollection<string> LanguageEnumS { get; set; }

        [JsonProperty("date_type_enum_s")]
        public ICollection<string> DateTypeEnumS { get; set; }

        [JsonProperty("label_enum_s")]
        public ICollection<string> LabelEnumS { get; set; }

        [JsonProperty("portion_enum_s")]
        public ICollection<string> PortionEnumS { get; set; }

        [JsonProperty("name_order_enum_s")]
        public ICollection<string> NameOrderEnumS { get; set; }

        [JsonProperty("term_type_enum_s")]
        public ICollection<string> TermTypeEnumS { get; set; }

        [JsonProperty("subjects")]
        public ICollection<string> Subjects { get; set; }

        [JsonProperty("subjects_text")]
        public ICollection<string> SubjectsText { get; set; }

        [JsonProperty("agents")]
        public ICollection<string> Agents { get; set; }

        [JsonProperty("agents_text")]
        public ICollection<string> AgentsText { get; set; }

        [JsonProperty("agent_uris")]
        public ICollection<string> AgentUris { get; set; }

        [JsonProperty("creators")]
        public ICollection<string> Creators { get; set; }

        [JsonProperty("creators_text")]
        public ICollection<string> CreatorsText { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("finding_aid_title")]
        public string FindingAidTitle { get; set; }

        [JsonProperty("finding_aid_filing_title")]
        public string FindingAidFilingTitle { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("restrictions")]
        public string Restrictions { get; set; }

        [JsonProperty("ead_id")]
        public string EadId { get; set; }

        [JsonProperty("finding_aid_status")]
        public string FindingAidStatus { get; set; }

        [JsonProperty("external_id")]
        public ICollection<string> ExternalId { get; set; }

        [JsonProperty("four_part_id")]
        public string FourPartId { get; set; }
    }

}