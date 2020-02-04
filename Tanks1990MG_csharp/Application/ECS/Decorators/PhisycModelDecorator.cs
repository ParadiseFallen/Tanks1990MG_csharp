using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Fabric;
using ECS.Fabric.Decorators;
using EMCS.Interfaces.Entity;
using Tanks1990MG_csharp.Application.ECS.Components;
using Tanks1990MG_csharp.Application.GameEntityes.Components;
using Tanks1990MG_csharp.Application.Interfaces;

namespace Tanks1990MG_csharp.Application.ECS.Fabric.Decorators
{
    public class PhisycModelDecorator : Decorator
    {
        override public void Decorate(IEntity entity)
        {
            entity.Components.AddComponent(new PhisycComponent());
        }
    }
}
