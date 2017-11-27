using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    public class Instance
    {
        [JsonProperty("lock_version")]
        public int LockVersion { get; set; }

        [JsonProperty("instance_type")]
        public string InstanceType { get; set; }

        [JsonProperty("container")]
        public Container Container { get; set; }

        [JsonProperty("sub_container")]
        public SubContainer SubContainer { get; set; }

        [JsonProperty("digital_object")]
        public dynamic DigitalObject { get; set; } //ref

        [JsonProperty("is_representative")]
        public bool IsRepresentative { get; set; }
    }
}