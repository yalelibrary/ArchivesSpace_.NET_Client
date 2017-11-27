using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class FileVersion
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("file_uri")]
        public string FileUri { get; set; }

        [JsonProperty("publish")]
        public bool Publish { get; set; }

        [JsonProperty("use_statement")]
        public string UseStatement { get; set; }

        [JsonProperty("xlink_actuate_attribute")]
        public string XlinkActuateAttribute { get; set; }

        [JsonProperty("xlink_show_attribute")]
        public string XlinkShowAttribute { get; set; }

        [JsonProperty("file_format_name")]
        public string FileFormatName { get; set; }

        [JsonProperty("file_format_version")]
        public string FileFormatVersion { get; set; }

        [JsonProperty("file_size_bytes")]
        public int FileSizeBytes { get; set; }

        [JsonProperty("is_representative")]
        public bool IsRepresentative { get; set; }

        [JsonProperty("checksum")]
        public string Checksum { get; set; }

        [JsonProperty("checksum_method")]
        public string ChecksumMethod { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }
    }
}
