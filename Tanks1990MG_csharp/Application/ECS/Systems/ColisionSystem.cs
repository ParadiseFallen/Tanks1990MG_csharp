using EMCS.Interfaces;
using EMCS.Interfaces.Entity;
using EMCS.Realisations.Signatures;
using EMCS.Systems.SubSystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.ECS.Components;

namespace Tanks1990MG_csharp.Application.ECS.Systems
{
    class ColisionSystem2D :ECSSystem
    {
        
        public override void Update(GameTime gameTime)
        {
            foreach (var item in EntityComponents)
            {
                //(item as ColisionComponent2D).ColisionArea.Y= (item as ColisionComponent2D).pComponent.Position.Y;
                for (int i = 1; i < EntityComponents.Count; i++)
                {
                    if (item == EntityComponents[i])
                        continue;
                    if ((EntityComponents[i] as ColisionComponent2D).ColisionBound.Intersects((item as ColisionComponent2D).ColisionBound))
                    {
                        (item as ColisionComponent2D).InitColision(EntityComponents[i] as ColisionComponent2D);
                        (EntityComponents[i] as ColisionComponent2D).InitColision(item as ColisionComponent2D);
                    }
                }
            }
            base.Update(gameTime);
        }
        public ColisionSystem2D()
        {
            OnAddComponent += (s,a) => { (a as ColisionComponent2D).OnColision += (s2,a2) => {
                if (s2.NameObject == "Tank" || s2.NameObject == "Wall" && a2.NameObject == "Bullet" || a2.NameObject == "Tank" || a2.NameObject == "Wall" && s2.NameObject == "Bullet")
                    DeleteEntityColision?.Invoke(a.Parent);
                a.Parent.Components.GetComponent<PhisycComponent>().Position -=a.Parent.Components.GetComponent<PhisycComponent>().Acceleration*2;
                }; };
            OnRemoveComponent += (s, a) => {
                (a as ColisionComponent2D).OnColision -= (s2, a2) => {
                    if (s2.NameObject == "Tank" || s2.NameObject == "Wall" && a2.NameObject == "Bullet" || a2.NameObject == "Tank" || a2.NameObject == "Wall" && s2.NameObject == "Bullet")
                        DeleteEntityColision?.Invoke(a.Parent);
                    a.Parent.Components.GetComponent<PhisycComponent>().Position -= a.Parent.Components.GetComponent<PhisycComponent>().Acceleration * 2;
                };
            };
        }

        public event Action<IEntity> DeleteEntityColision;
        public override Type TargetComponent => typeof(ColisionComponent2D);
        public override ComponentsSignature TargetSignature => new ComponentsSignature(typeof(PhisycComponent),typeof(ColisionComponent2D));

    }
}
