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

namespace Tanks1990MG_csharp.Application.ECS.Systems
{
    class PhisycSystem : ECSSystem
    {
        public override Type TargetComponent => typeof(PhisycComponent);
        public override ComponentsSignature TargetSignature => new ComponentsSignature(typeof(IEntity),typeof(PhisycComponent));
        public override void Update(GameTime gameTime)
        {
            EntityComponents.ForEach(i=> {
                var component = (i as PhisycComponent);
                component.Position += component.Acceleration /* * gameTime*/;
                component.Acceleration = Vector3.Lerp(component.Acceleration,Vector3.Zero,0.5f);
                Console.WriteLine($"Acc: {component.Acceleration}");
            });
        }
    }
}
