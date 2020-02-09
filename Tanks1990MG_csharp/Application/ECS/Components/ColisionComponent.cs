using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMCS.Interfaces.Entity;
using Microsoft.Xna.Framework;

namespace Tanks1990MG_csharp.Application.ECS.Components
{
    class ColisionComponent2D : EMCS.Realisations.Components.Component
    {
        public PhisycComponent pComponent;
        public string NameObject { get; set; }
        public override void WayToAtivate(IEntity parrent)
        {
            pComponent = parrent.Components.GetComponent<PhisycComponent>();
            if (pComponent!= null)
                IsActivated = true;
            base.WayToAtivate(parrent);
        }

        public override void WayToDeativate(IEntity parrent)
        {
            IsActivated = false;
            base.WayToDeativate(parrent);
        }

        public Rectangle ColisionArea { get; set; }
        public Rectangle ColisionBound { get { return new Rectangle((int)pComponent.Position.X+ColisionArea.X, (int)pComponent.Position.Y+ColisionArea.Y, ColisionArea.Width, ColisionArea.Height); } }
        public void InitColision(ColisionComponent2D other)
        {
            OnColision?.Invoke(this, other);
        }

        public event Action<ColisionComponent2D, ColisionComponent2D> OnColision;

    }
}
