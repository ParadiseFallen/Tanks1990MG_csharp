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
    public class EntitySystemMONOGAME : IEntitySystem, IUpdateable
    {
        //systems
        public ISystemStorage Systems { get; } = new SystemStorage();
        //entities
        public IChildContainer<IEntity> Entities { get; } = new EntityContainer();

        public bool Enabled { get; set; } = true;

        public int UpdateOrder { get; set; } = 0;
        /*Подписка на добавления*/
        private void AddRegisterEntity(object sender, IEntity entity)
        {
            Systems.Systems.ForEach(i =>
            {
                //жобавиь новую сущность
                i.AddEntity(entity);
                //this.Entities.AddChild(entity);

                entity.Childs.OnChildAdded += AddRegisterEntity;
                entity.Childs.OnChildRemoved += RemoveRegisterEntity;

                //если есть дети - добавить детей
                if (entity.Childs != null)
                    foreach (var item in entity.Childs.ChildEntities)
                    {
                        AddRegisterEntity(this,item);
                    }
            });
        }
        /*Подписка на удаление*/
        private void RemoveRegisterEntity(object sender, IEntity entity)
        {
            Systems.Systems.ForEach(i =>
            {
                //жобавиь новую сущность
                i.RemoveEntity(entity);
                //this.Entities.RemoveChild(entity);
                //если есть дети - добавить детей
                if (entity.Childs != null)
                    foreach (var item in entity.Childs.ChildEntities)
                    {
                        RemoveRegisterEntity(this, item);
                    }

                entity.Childs.OnChildAdded -= AddRegisterEntity;
                entity.Childs.OnChildAdded -= RemoveRegisterEntity;
            });
        }

        public EntitySystemMONOGAME()
        {
            //При добавлении какой либо новой системы мы говорим что Entities.OnChildAdded будет вызывать метод добавления сущности в эту систему
            //Systems.OnAddSystem += (mainSystem, NewAddedSystem) =>
            //{
            //    //Entities.OnChildAdded += OnAddEntity;
            //    //Теперь добавление в Entities будет вызывать добавление в систему
            //    //При добавлении в систему
            //    //Entities.OnChildAdded += (container,child)=> { AddSystemLink(NewAddedSystem, container, child); };

            //    //Entities.OnChildRemoved -= (container, child) => { AddSystemLink(NewAddedSystem, container, child); };
            //};

            Entities.OnChildAdded += AddRegisterEntity;
            Entities.OnChildRemoved += RemoveRegisterEntity;

            //Systems.OnRemoveSystem += (Ss, Sa) =>
            //{
            //    Entities.OnChildAdded -= (Esender, Earg) =>
            //    {
            //        Earg.Childs.OnChildAdded -= (s, a) => { Sa.AddEntity(Earg); };
            //        Sa.AddEntity(Earg);
            //    };
            //    Entities.OnChildRemoved -= (Esender, Earg) =>
            //    {
            //        Earg.Childs.OnChildRemoved -= (s, a) => { Sa.AddEntity(Earg); };
            //        Sa.RemoveEntity(Earg);
            //    };
            //};

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
