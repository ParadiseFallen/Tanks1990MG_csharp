using EMCS.Interfaces.Entity;
using EMCS.Realisations.Signatures;
using EMCS.Systems.SubSystems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.ECS.Components;
using Tanks1990MG_csharp.Application.ECS.Dependencies;

namespace Tanks1990MG_csharp.Application.ECS.Systems
{
    class RenderSystem : ECSSystem
    {

        public override void Update(GameTime gameTime)
        {
            foreach (RenderComponent item in EntityComponents)
            {
                if (item.phisycComponent == null)
                    continue;
                item.Source.Position = item.phisycComponent.Position;
                item.Source.Rotation = item.phisycComponent.Rotation;
            }
            base.Update(gameTime);
        }

        public RenderSystem()
        {
            OnAddComponent += (s, a) => { 
                Application.RenderSystem.Instance.Drawables.Add((a as RenderComponent).Source);
                //Console.WriteLine("Add");
            };
            OnRemoveComponent += (s, a) => { Application.RenderSystem.Instance.Drawables.Remove((a as RenderComponent).Source); };
        }
        public override Type TargetComponent => typeof(RenderComponent);
        public override ComponentsSignature TargetSignature => new ComponentsSignature(typeof(RenderComponent));
    }
}
