using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class RightsStatement
    {
        [JsonProperty("rights_type")]
        public string RightsType { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("active")]
        public string Active { get; set; }

        [JsonProperty("materials")]
        public string Materials { get; set; }

        [JsonProperty("ip_status")]
        public string IpStatus { get; set; }

        [JsonProperty("ip_expiration_date")]
        public Date IpExpirationDate { get; set; }

        [JsonProperty("license_identifier_terms")]
        public string LicenceIdentifierTerms { get; set; }

        [JsonProperty("statute_citation")]
        public string StatuteCitation { get; set; }

        [JsonProperty("jurisdiction")]
        public string Jurisdiction { get; set; }

        [JsonProperty("type_note")]
        public string TypeNote { get; set; }

        [JsonProperty("permissions")]
        public string Permissions { get; set; }

        [JsonProperty("restrictions")]
        public string Restrictions { get; set; }

        [JsonProperty("restriction_start_date")]
        public Date RestrictionStartDate { get; set; }

        [JsonProperty("restriction_end_date")]
        public Date RestrictionEndDate { get; set; }

        [JsonProperty("granted_note")]
        public string GrantedNote { get; set; }

        [JsonProperty("external_documents")]
        public ICollection<ExternalDocument> ExternalDocuments { get; set; }
    }
}