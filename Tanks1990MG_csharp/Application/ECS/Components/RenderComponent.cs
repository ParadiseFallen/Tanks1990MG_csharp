using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMCS.Interfaces.Entity;
using Tanks1990MG_csharp.Application.ECS.Dependencies;

namespace Tanks1990MG_csharp.Application.ECS.Components
{
    class RenderComponent : EMCS.Realisations.Components.Component
    {
        public IAutoDraw Source { get; set; } = null;

        public PhisycComponent phisycComponent;
        public override void WayToAtivate(IEntity parrent)
        {
            phisycComponent = parrent.Components.GetComponent<PhisycComponent>();
            IsActivated = true;
            base.WayToAtivate(parrent);
        }
        
        public override void WayToDeativate(IEntity parrent)
        {
            phisycComponent = null;
            IsActivated = false;
            base.WayToDeativate(parrent);
        }
        public override void Reset()
        {
            Deactivate(Parent);
            base.Reset();
        }
    }
}
