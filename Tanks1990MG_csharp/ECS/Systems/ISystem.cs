using EMCS.Interfaces.Entity;
using EMCS.Realisations.Signatures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMCS.Interfaces.System
{
    public interface ISystem : IUpdateable
    {
        ComponentsSignature TargetSignature { get; }
        Type TargetComponent { get; }
        void AddEntity(IEntity gameEntity);
        void RemoveEntity(IEntity gameEntity);
        void Start();
        void Stop();
        //для чистых компонентов без поведения
        List<IEntityComponent<IEntity>> EntityComponents { get; }
        event Action<object, IEntityComponent<IEntity>> OnAddComponent;
        event Action<object, IEntityComponent<IEntity>> OnRemoveComponent;
    }
}