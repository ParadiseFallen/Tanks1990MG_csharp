using EMCS.Interfaces.Entity;
using EMCS.Realisations.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.ECS.Dependencies;

namespace Tanks1990MG_csharp.Application.ECS.Components
{
    class EntityController : Component
    {
        private PhisycComponent PComponent;
        private RenderComponent RComponent;
        public event Action<EntityController> OnShot;
        public override void WayToAtivate(IEntity parrent)
        {
            PComponent = parrent.Components.GetComponent<PhisycComponent>();
            RComponent = parrent.Components.GetComponent<RenderComponent>();
            if (PComponent != null && RComponent != null)
                IsActivated = true;
            base.WayToAtivate(parrent);
        }


        public void Move(Vector3 Direction)
        {
            PComponent.Acceleration += Direction;


            (RComponent.Source as Sprite2D).RotationDeg = (float)Math.Atan2(Direction.X ,- Direction.Y);
        }
        public void Shot()
        {
            OnShot?.Invoke(this);
        }
        public override void WayToDeativate(IEntity parrent)
        {
            IsActivated = false;
            base.WayToDeativate(parrent);
        }

    }
}
