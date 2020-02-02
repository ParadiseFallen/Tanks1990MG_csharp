using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMCS.Interfaces.Entity;
using Tanks1990MG_csharp.Application.GameEntityes.Solutions.Fabric.Builders;

namespace Tanks1990MG_csharp.Application.GameEntityes.Solutions.Fabric.Decorators
{
    class GUID_Decorator : IEntityDecorator
    {
        static private int _count;

        public EntityBuilder Builder { get ; set ; }

        public void Decorate(IGameEntity entity)
        {
            entity.GUID = _count++;
        }
    }
}
