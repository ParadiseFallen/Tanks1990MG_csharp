using ECS.Fabric;
using ECS.Fabric.Decorators;
using EMCS.Interfaces.Entity;

namespace Tanks1990MG_csharp.Application.ECS.Fabric.Decorators
{
    class GUID_Decorator : Decorator
    {
        static private int _count = 0;

        override public void Decorate(IEntity entity)
        {
            entity.GUID = _count++;
        }
    }
}
