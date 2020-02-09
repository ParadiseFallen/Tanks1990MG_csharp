using EMCS.Realisations.Signatures;
using EMCS.Systems.SubSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.ECS.Components;

namespace Tanks1990MG_csharp.Application.ECS.Systems
{
    class NetworkSystem : ECSSystem
    {
        public override Type TargetComponent => typeof(NetworkComponent);
        public override ComponentsSignature TargetSignature => new ComponentsSignature(typeof(NetworkComponent));

        
    }
}
