using EMCS.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.GameEntityes.Interfaces;

namespace ECS.Fabric.Decorators{
    public interface IEntityDecorator
    {
        EntityBuilder Builder { get; set; }
        void Decorate(IEntity entity);
    }
}
