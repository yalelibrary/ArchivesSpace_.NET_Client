using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ArchivesSpace_.Net_Client.Models
{
    class SearchAdvancedQueryBoolean : SearchAdvancedQueryBase
    {
        [JsonProperty("op")]
        public string Op { get; set; } //Operator, values are AND/OR/NOT

        [JsonProperty("subqueries")]
        public SearchAdvancedQueryBase Subqueries { get; set; }
    }
}
