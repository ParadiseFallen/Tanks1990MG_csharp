using EMCS.Interfaces;
using EMCS.Interfaces.Entity;
using EMCS.Interfaces.System;
using EMCS.Realisations.Containers;
using EMCS.Realisations.System;
using EMCS.Systems.MainSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Systems.MainSystem
{
    public class EntitySystemMONOGAME : IEntitySystem,IUpdateable
    {
        //systems
        public ISystemStorage Systems { get; } = new SystemStorage();
        //entities
        public IChildContainer<IEntity> Entities { get; } = new EntityContainer();

        public bool Enabled { get; set; } = true;

        public int UpdateOrder { get; set; } = 0;
        public EntitySystemMONOGAME()
        {
            //При добавлении какой либо новой системы мы говорим что Entities.OnChildAdded будет вызывать метод добавления сущности в эту систему
            Systems.OnAddSystem += (Ss, Sa) => {
                //Теперь добавление в Entities будет вызывать добавление в систему
                //При добавлении в систему
                Entities.OnChildAdded += (Esender, Earg) => {
                    Earg.Childs.OnChildAdded += (s, a) => { Sa.AddEntity(Earg); };
                    Sa.AddEntity(Earg);
                };
                //Теперь добавление в Entities будет вызывать удаление из системы
                Entities.OnChildRemoved += (Esender, Earg) => {
                    Earg.Childs.OnChildRemoved += (s, a) => { Sa.AddEntity(Earg); };
                    Sa.RemoveEntity(Earg);
                };
            };
            Systems.OnRemoveSystem += (Ss, Sa) => {
                Entities.OnChildAdded -= (Esender, Earg) => {
                    Earg.Childs.OnChildAdded -= (s, a) => { Sa.AddEntity(Earg); };
                    Sa.AddEntity(Earg);
                };
                Entities.OnChildRemoved -= (Esender, Earg) => {
                    Earg.Childs.OnChildRemoved -= (s, a) => { Sa.AddEntity(Earg); };
                    Sa.RemoveEntity(Earg);
                };
            };

        }

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        virtual public void Start() { }
        virtual public void Stop() { }
        virtual public void Update(GameTime gameTime)
        {
            Systems.Systems.ForEach(i => i.Update(gameTime));
        }
    }
}
