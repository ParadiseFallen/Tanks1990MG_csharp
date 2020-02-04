using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Fabric
{
    [Serializable]
    public class DecorationChain
    {
        public List<string> DecoratorsChain { get; } = new List<string>();
        public string EntityProviderName { get; set; } = "";
    }
}
