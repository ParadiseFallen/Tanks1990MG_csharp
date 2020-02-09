using ECS.Fabric.Decorators;
using EMCS.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.ECS.Components;

namespace Tanks1990MG_csharp.Application.ECS.FabricDecorators
{
    class ControlDecorator : Decorator
    {
        public override void Decorate(IEntity entity)
        {
            entity.Components.AddComponent(new EntityController());
            base.Decorate(entity);
        }
    }
}
