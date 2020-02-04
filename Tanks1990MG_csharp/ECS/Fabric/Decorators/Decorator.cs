using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMCS.Interfaces.Entity;

namespace ECS.Fabric.Decorators
{
    public class Decorator : IEntityDecorator
    {
        public EntityBuilder Builder { get; set; }
        virtual public void Decorate(IEntity entity)
        {
        }
    }
}
