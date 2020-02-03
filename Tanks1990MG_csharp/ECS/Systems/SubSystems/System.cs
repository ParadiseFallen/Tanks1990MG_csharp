using EMCS.Interfaces;
using EMCS.Interfaces.Entity;
using EMCS.Interfaces.System;
using EMCS.Realisations.Signatures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMCS.Systems.SubSystems
{
    class System : ISystem
    {
        virtual public ComponentsSignature Target { get; }
        public List<IEntityComponent<IEntity>> EntityComponents { get; } = new List<IEntityComponent<IEntity>>();
        public bool Enabled { get; set; }
        public int UpdateOrder { get; set; }

        virtual public event EventHandler<EventArgs> EnabledChanged;
        virtual public event EventHandler<EventArgs> UpdateOrderChanged;
        virtual public event Action<object, IEntityComponent<IEntity>> OnAddComponent;
        virtual public event Action<object, IEntityComponent<IEntity>> OnRemoveComponent;

        public void AddEntity(IEntity gameEntity)
        {
            if (!gameEntity.Components.ComponentsTypeSignature.Equals(Target))
                return;
            foreach (var item in gameEntity.Components.Components)
            {
                if (Target.Signature.Contains(item.GetType()))
                { 
                    EntityComponents.Add(item);
                    OnAddComponent?.Invoke(this,item);
                }
            }
        }

        public void RemoveEntity(IEntity gameEntity)
        {
            foreach (var item in gameEntity.Components.Components)
            {
                if (Target.Signature.Contains(item.GetType()))
                {
                    EntityComponents.Remove(item);
                    OnRemoveComponent?.Invoke(this, item);
                }
            }
        }

        virtual public void Start()
        {
        }

        virtual public void Stop()
        {
        }

        virtual public void Update(GameTime gameTime)
        {
        }
    }
}
