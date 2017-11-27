using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArchivesSpace_.Net_Client.Models
{
    public class SearchResultEntryTopContainer : SearchResultEntryBase
    {
        private TopContainer _deserializedJson;
        
        [JsonIgnore]
        public virtual TopContainer ParsedJson
        {
            get
            {
                if (_deserializedJson == null)
                {
                    _deserializedJson = JsonConvert.DeserializeObject<TopContainer>(Json);
                }
                return _deserializedJson;
            }
        }

        [JsonProperty("display_string")]
        public string DisplayString { get; set; }

        [JsonProperty("series_uri_u_sstr")]
        public ICollection<string> SeriesUriUSstr { get; set; }

        [JsonProperty("series_title_u_sstr")]
        public ICollection<string> SeriesTitleUSstr { get; set; }

        [JsonProperty("series_level_u_sstr")]
        public ICollection<string> SeriesLevelUSstr { get; set; }

        [JsonProperty("series_identifier_stored_u_sstr")]
        public ICollection<string> SeriesIdentifierStoredUSstr { get; set; }

        [JsonProperty("series_identifier_u_stext")]
        public ICollection<string> SeriesIdentifierUStext { get; set; }

        [JsonProperty("collection_uri_u_sstr")]
        public ICollection<string> CollectionUriUSstr { get; set; }

        [JsonProperty("collection_display_string_u_sstr")]
        public ICollection<string> CollectionDisplayStringUSstr { get; set; }

        [JsonProperty("collection_identifier_stored_u_sstr")]
        public ICollection<string> CollectionIdentifierStoredUSstr { get; set; }

        [JsonProperty("collection_identifier_u_stext")]
        public ICollection<string> CollectionIdentifierUStext { get; set; }

        [JsonProperty("container_profile_uri_u_sstr")]
        public ICollection<string> ContainerProfileUriUSstr { get; set; }

        [JsonProperty("container_profile_display_string_u_sstr")]
        public ICollection<string> ContainerProfileDisplayStringUSstr { get; set; }

        [JsonProperty("location_uri_u_sstr")]
        public ICollection<string> LocationUriUSstr { get; set; }

        [JsonProperty("location_uris")]
        public ICollection<string> LocationUris { get; set; }

        [JsonProperty("location_display_string_u_sstr")]
        public ICollection<string> LocationDisplayStringUSstr { get; set; }

        [JsonProperty("exported_u_sbool")]
        public ICollection<bool> ExportedUSbool { get; set; }

        [JsonProperty("empty_u_sbool")]
        public ICollection<bool> EmptyUSbool { get; set; }

        [JsonProperty("barcode_u_sstr")]
        public ICollection<string> BarcodeUSstr { get; set; }

    }
}