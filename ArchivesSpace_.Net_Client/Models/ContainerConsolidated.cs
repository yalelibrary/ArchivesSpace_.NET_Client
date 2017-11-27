using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchivesSpace_.Net_Client.Models
{
    /// <summary>
    /// This model does not exist in ArchivesSpace and is used here to create a more logical representation of containter/subcontainer relationships.
    /// </summary>
    public class ContainerConsolidated : Container
    {
        public ICollection<SubContainer> SubContainers { get; set; }
        public int TopContainerId { get; set; }
    }
}
