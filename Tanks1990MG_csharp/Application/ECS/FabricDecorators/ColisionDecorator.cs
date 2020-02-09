using ECS.Fabric.Decorators;
using EMCS.Interfaces.Entity;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.ECS.Components;
using Tanks1990MG_csharp.Application.ECS.Dependencies;

namespace Tanks1990MG_csharp.Application.ECS.FabricDecorators
{
    class ColisionDecorator : Decorator
    {

        public override void Decorate(IEntity entity)
        {
            var t = new ColisionComponent2D() { ColisionArea = new Microsoft.Xna.Framework.Rectangle(0, 0,32 , 32) };
            entity.Components.AddComponent(t) ;
            base.Decorate(entity);
        }
    }
}
