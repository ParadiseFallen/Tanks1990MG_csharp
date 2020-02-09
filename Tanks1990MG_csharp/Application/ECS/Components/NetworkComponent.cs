using EMCS.Interfaces.Entity;
using EMCS.Realisations.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990MG_csharp.Application.ECS.Components
{
    class NetworkComponent : Component
    {

        public override void WayToAtivate(IEntity parrent)
        {
            foreach (var item in Parent.Components.Components)
            {

            }
            base.WayToAtivate(parrent);
        }

    }
}
