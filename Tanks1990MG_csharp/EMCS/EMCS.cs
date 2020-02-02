using EMCS.Interfaces;
using EMCS.Interfaces.Components.Component;
using EMCS.Interfaces.Components.Model;
using EMCS.Interfaces.Entity;
using EMCS.Interfaces.System;
using EMCS.Realisations.Signatures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;








namespace EMCS.Interfaces.System
{
    public interface ISystemBehavior
    { 
        Action<IEntityComponent<IGameEntity>,GameTime> UpdateComponentDefault { get; set; }
        Action<IEntityComponent<IGameEntity>,GameTime> UpdateModel { get; set; }
    }
    public interface ISystemStorage
    { 
        List<ISystem> Systems { get; }
        //получить уже типизированный компонент по его сигнатуре
        T GetSystem<T>() where T :class, ISystem;
        //получить компонент по сигнатуре
        ISystem GetSystem(Type SystemType);
        //получить компонент по экземпляру
        ISystem GetSystem(ISystem System);
        //добавить компонент
        bool AddSystem(ISystem component);
        //убрать компонент
        bool RemoveSystem(ISystem component);

        event Action<object, ISystem> OnAddSystem;
        event Action<object, ISystem> OnRemoveSystem;
    }
    public interface ISystem : IUpdateable
    {
        ComponentsSignature Target { get;  }
        void AddEntity(IGameEntity gameEntity);
        void RemoveEntity(IGameEntity gameEntity);
        ISystemBehavior SystemBehavior { get;  }
        //для чистых компонентов без поведения
        Lazy<List<IEntityComponent<IGameEntity>>> EntitiesComponents { get; }
        //для компонентов с поведением
        Lazy<List<IEntityComponent<IGameEntity>>> EntitiesComponentsWithBehavior { get; }
    }

    public interface IMainSystem : IUpdateable
    {
        ISystemStorage Systems { get; }
        IChildContainer<IGameEntity> Entities { get; }
    }
}
namespace EMCS.Interfaces.Entity
{
    public interface IGameEntity : IChildContainer<IGameEntity>, IHaveParent<IGameEntity>
    {
        //может быть TAG
        string TAG { get; set; }
        //может быть GUID
        int GUID { get; set; }
        IComponentsContainer Components { get; }
    }
}
namespace EMCS.Realisations.Entity
{
    public class CustomEntity : IGameEntity
    {
        #region Props
        public string TAG { get; set; } = "";
        public int GUID { get; set; } = -1;
        //Components
        public IComponentsContainer Components { get; set; } 
        //childs
        public List<IGameEntity> ChildEntities { get; } = new List<IGameEntity>();
        //parrent
        public IGameEntity ParentEntity { get; set; } = null;
        #endregion
        #region Events
        public event Action<object, IEntityComponent<IGameEntity>> OnAddComponent;
        public event Action<object, IEntityComponent<IGameEntity>> OnRemoveComponent;
        public event Action<object, IGameEntity> OnChildAdded;
        public event Action<object, IGameEntity> OnChildRemoved;
        #endregion
        #region Methods
        virtual public void AddChild(IGameEntity entity)
        {
            ChildEntities.Add(entity);
            OnChildAdded?.Invoke(this, entity);
        }

        virtual public bool AddComponent(IEntityComponent<IGameEntity> component)
        {
            var t = Components.AddComponent(component);
            OnAddComponent?.Invoke(this, component);
            return t;
        }

        virtual public IGameEntity GetChild(Func<IGameEntity, bool> Filter)
        {
            foreach (var item in ChildEntities)
            {
                if (Filter(item))
                    return item;
            }
            return default;
        }

        virtual public T GetComponent<T>() where T :class, IEntityComponent<IGameEntity>
        {
            return Components.GetComponent<T>();
        }

        virtual public IEntityComponent<IGameEntity> GetComponent(Type ComponentType)
        {
            return Components.GetComponent(ComponentType);
        }

        virtual public IEntityComponent<IGameEntity> GetComponent(IEntityComponent<IGameEntity> Component)
        {
            return Components.GetComponent(Component);
        }

        virtual public IGameEntity RecursiveGetChild(Func<IGameEntity, bool> Filter, int depth = 10)
        {
            IGameEntity res = null;

            if (depth < 0)
                return null;
            foreach (var item in ChildEntities)
            {
                if (Filter(item))
                    return item;

                res = item.RecursiveGetChild(Filter, depth--);
                if (res != null)
                    return res;
            }
            return res;
        }

        virtual public bool RemoveChild(IGameEntity entity)
        {
            return ChildEntities.Remove(entity);
        }

        public bool RemoveComponent(IEntityComponent<IGameEntity> component)
        {
            return Components.RemoveComponent(component);
        }

        #endregion
    }
}
namespace EMCS.Realisations.Signatures
{
    //Класс для работы с сигнатурой компонентов
    public class ComponentsSignature : IEquatable<ComponentsSignature>
    {
        //Компонентная сигнатура
        public List<Type> Signature { get; set; } = new List<Type>();
        //создать сигнатруру
        public ComponentsSignature(params Type[] componentTypeSignature)
        {
            foreach (var item in componentTypeSignature)
            {
                Signature.Add(item);
            }
        }
        //создать сигнатруру
        public ComponentsSignature(List<IEntityComponent<IGameEntity>> Signature)
        {
            Signature.ForEach(
                i => { this.Signature.Add(i.GetType()); }
                );
        }
        //создать сигнатруру
        public ComponentsSignature(List<Type> Signature)
        {
            this.Signature = Signature;
        }
        //создать сигнатруру 
        private ComponentsSignature(List<Type> SignatureA, List<Type> SignatureB, bool Add)
        {
            if (Add)
            {
                Signature.AddRange(SignatureA);
                Signature.AddRange(SignatureB);
            }
            else
            {
                Signature = SignatureA.Except(SignatureB).ToList();
            }
        }
        //эквивалентны ли сигнатруы
        public bool Equals(ComponentsSignature other)
        {
            foreach (var item in Signature)
            {
                if (!other.Signature.Contains(item))
                    return false;
            }
            return true;
        }
        public static bool operator !=(ComponentsSignature left, ComponentsSignature right) => !left.Equals(right);
        public static bool operator ==(ComponentsSignature left, ComponentsSignature right) => left.Equals(right);
        public static ComponentsSignature operator +(ComponentsSignature left, ComponentsSignature right) => new ComponentsSignature(left.Signature, right.Signature, true);
        public static ComponentsSignature operator -(ComponentsSignature left, ComponentsSignature right) => new ComponentsSignature(left.Signature, right.Signature, false);

    }

}
namespace EMCS.Realisations.System
{

    public class BetterSystemStorage : ISystemStorage
    {
        public List<ISystem> Systems { get; } = new List<ISystem>();
        public event Action<object, ISystem> OnAddSystem;
        public event Action<object, ISystem> OnRemoveSystem;

        /// <summary>
        /// Add new System
        /// </summary>
        /// <param name="system">New system to add</param>
        /// <returns>Sucsess or not</returns>
        public bool AddSystem(ISystem system)
        {
            if (Systems.Contains(system))
            return false;
            Systems.Add(system);
            OnAddSystem.Invoke(this, system);
            return true;
        }
        public T GetSystem<T>() where T :class, ISystem
        {

            foreach (var item in Systems)
            {
                if (item is T)
                    return item as T;
            }
            return default;
        }
        public ISystem GetSystem(Type SystemType)
        {
            foreach (var item in Systems)
            {
                if (item.GetType() == SystemType)
                    return item;
            }

            return default;
        }
        public ISystem GetSystem(ISystem System)
        {
            foreach (var item in Systems)
            {
                if (item == System)
                    return item;
            }
            return default;
        }
        public bool RemoveSystem(ISystem system)
        {
            return Systems.Remove(system);
        }
    }

    public class EntityContainer : IChildContainer<IGameEntity>
    {
        public List<IGameEntity> ChildEntities { get; } = new List<IGameEntity>();
        public event Action<object, IGameEntity> OnChildAdded;
        public event Action<object, IGameEntity> OnChildRemoved;
        public void AddChild(IGameEntity entity)
        {
            ChildEntities.Add(entity);
            OnChildAdded?.Invoke(this, entity);
        }
        public IGameEntity GetChild(Func<IGameEntity, bool> Filter)
        {
            foreach (var item in ChildEntities)
            {
                if (Filter(item))
                    return item;
            }
            return default;
        }
        public IGameEntity RecursiveGetChild(Func<IGameEntity, bool> Filter, int depth = 10)
        {
            if (depth < 0)
                return default;
            IGameEntity entity = null;
            foreach (var item in ChildEntities)
            {
                if (Filter(item))
                    return item;
            }

            foreach (var item in ChildEntities)
            {
                entity = item.RecursiveGetChild(Filter,depth--);
                if (entity != null)
                    return entity;
            }

            return default;
        }
        public bool RemoveChild(IGameEntity entity)
        {
            return ChildEntities.Remove(entity);
        }
    }

    public class EntitySystem : IMainSystem
    {
        //systems
        public ISystemStorage Systems { get; } = new BetterSystemStorage();
        //entities
        public IChildContainer<IGameEntity> Entities { get; } = new EntityContainer();

        public bool Enabled { get; set; } = true;

        public int UpdateOrder { get; set; } = 0;
        public EntitySystem()
        {
            //При добавлении какой либо новой системы мы говорим что Entities.OnChildAdded будет вызывать метод добавления сущности в эту систему
            Systems.OnAddSystem += (Ss, Sa) => {
                //Теперь добавление в Entities будет вызывать добавление в систему
                //При добавлении в систему
                Entities.OnChildAdded += (Esender,Earg)=> {
                    Earg.OnChildAdded += (s, a) => { Sa.AddEntity(Earg); };
                    Sa.AddEntity( Earg);
                };
                //Теперь добавление в Entities будет вызывать удаление из системы
                Entities.OnChildRemoved += (Esender, Earg) => {
                    Earg.OnChildRemoved += (s, a) => { Sa.AddEntity(Earg); };
                    Sa.RemoveEntity( Earg); 
                };
            };
            Systems.OnRemoveSystem += (Ss, Sa) => {
                Entities.OnChildAdded -= (Esender,Earg)=> {
                    Earg.OnChildAdded -= (s, a) => { Sa.AddEntity(Earg); };
                    Sa.AddEntity( Earg); 
                };
                Entities.OnChildRemoved -= (Esender, Earg) => {
                    Earg.OnChildRemoved -= (s, a) => { Sa.AddEntity(Earg); };
                    Sa.RemoveEntity( Earg); 
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
namespace EMCS.Realisations.Components.Model
{
    public class StandartModelBehavior : IModelBehavior
    {
        public IEntityComponentModel Model { get ; set ; }

        virtual public void Update(GameTime time) { }
    }

    public class StandartModel : IEntityComponentModel
    {
        private IModelBehavior _Behavior;
        public IModelBehavior Behavior { get { return _Behavior; } set { _Behavior = value; IsBehaviorSet = _Behavior is null ?  false : true;  } }

        public bool IsBehaviorSet { get; set; }

        public IGameEntity Parent { get; set; }

        public bool Activated { get; protected set; }

        public List<IEntityComponent<IGameEntity>> Components => throw new NotImplementedException();

        public ComponentsSignature ComponentsTypeSignature => throw new NotImplementedException();

        IGameEntity IComponentsContainer.Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        virtual public event Action<IEntityComponent<IGameEntity>> OnActivated;
        virtual public event Action<IEntityComponent<IGameEntity>> OnDeactevated;
        virtual public event Action<IEntityComponent<IGameEntity>> OnReset;

        virtual public event PropertyChangedEventHandler PropertyChanged;

        virtual public event Action<object, IEntityComponent<IGameEntity>> OnAddComponent;
        virtual public event Action<object, IEntityComponent<IGameEntity>> OnRemoveComponent;

        virtual public void WayToAtivate(IGameEntity parrent) { }
        virtual public void WayToDeativate(IGameEntity parrent) { }

        public void Activate(IGameEntity parrent)
        {
            Activated = true;
            foreach (var item in Components)
            {
                if (!item.Activated)
                {
                    item.Activate(Parent);
                    Activated = false;
                }
            }
            WayToAtivate(parrent);
        }
        
        public bool AddComponent(IEntityComponent<IGameEntity> component)
        {
            if (Components.Contains(component))
                return false;
            component.Activate(Parent);
            Components.Add(component);
            return true;
        }

        public void Deactivate(IGameEntity parrent)
        {
            foreach (var item in Components)
            {
                item.Deactivate(Parent);
            }
            Activated = false;
            WayToDeativate(parrent);
        }

        public IEntityComponent<IGameEntity> GetComponent(Type ComponentType)
        {
            foreach (var item in Components)
            {
                if (item.GetType() == ComponentType)
                    return item;
            }
            return default;
        }

        public IEntityComponent<IGameEntity> GetComponent(IEntityComponent<IGameEntity> Component)
        {
            foreach (var item in Components)
            {
                if (item == Component)
                    return item;
            }
            return default;
        }

        public bool RemoveComponent(IEntityComponent<IGameEntity> component)
        {
            return Components.Remove(component);
        }

        virtual public void Reset()
        {
        }

        public T GetComponent<T>() where T : class, IEntityComponent<IGameEntity>
        {
            foreach (var item in Components)
            {
                if (item is T)
                    return item as T;
            }
            return default;
        }
    }

    //public class CustomModel : IEntityComponentModel
    //{
    //    public IModelBehavior Behavior { get; set; }

    //    public bool IsBehaviorSet { get; set; } = false;

    //    public IGameEntity Parent { get; set; } = null;

    //    public bool Activated { get; } = false;


    //    public List<IEntityComponent<IGameEntity>> Components { get; }

    //    public ComponentsSignature ComponentsTypeSignature => new ComponentsSignature(Components);

    //    public event Action<IEntityComponent<IGameEntity>> OnTryActivate;
    //    public event Action<IEntityComponent<IGameEntity>> OnActivated;
    //    public event Action<IEntityComponent<IGameEntity>> OnTryDeactevated;
    //    public event Action<IEntityComponent<IGameEntity>> OnDeactevated;
    //    public event Action<IEntityComponent<IGameEntity>> OnReset;
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    public event Action<object, IEntityComponent<IGameEntity>> OnAddComponent;
    //    public event Action<object, IEntityComponent<IGameEntity>> OnRemoveComponent;

    //    public void Activate(IGameEntity parrent)
    //    {
    //        Parent = parrent;
    //        OnTryActivate?.Invoke(this);
    //        //your code here
    //        OnActivated?.Invoke(this);
    //    }

    //    public bool AddComponent(IEntityComponent<IGameEntity> component)
    //    {
    //        if (Components.Contains(component))
    //            return false;
    //        Components.Add(component);
    //        return true;
    //    }

    //    public void Deactivate(IGameEntity parrent)
    //    {
    //        OnTryActivate?.Invoke(this);
    //        //your code here
    //        OnActivated?.Invoke(this);
    //        Parent = null;
    //    }

    //    public IEntityComponent<IGameEntity> GetComponent(Type ComponentType)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEntityComponent<IGameEntity> GetComponent(IEntityComponent<IGameEntity> Component)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool RemoveComponent(IEntityComponent<IGameEntity> component)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Reset()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Update(GameTime gameTime)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    T IComponentsContainer.GetComponent<T>()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
namespace EMCS.Realisations.Components.Component
{
    public class ComponentContainer : IComponentsContainer
    {
        public List<IEntityComponent<IGameEntity>> Components { get; }
        public ComponentsSignature ComponentsTypeSignature { get; }
        public IGameEntity Parent { get; set; }

        public event Action<object, IEntityComponent<IGameEntity>> OnAddComponent;
        public event Action<object, IEntityComponent<IGameEntity>> OnRemoveComponent;
        public event Action<object, IEntityComponent<IGameEntity>> OnSetParent;

        public bool AddComponent(IEntityComponent<IGameEntity> component)
        {
            if (Components.Contains(component))
                return false;
            Components.Add(component);
            return true;
        }

        public T GetComponent<T>() where T :class, IEntityComponent<IGameEntity>
        {
            foreach (var item in Components)
            {
                if (item is T)
                    return item as T;
            }
            return null;
        }

        public IEntityComponent<IGameEntity> GetComponent(Type ComponentType)
        {
            foreach (var item in Components)
            {
                if (item.GetType()== ComponentType)
                    return item;
            }
            return null;
        }

        public IEntityComponent<IGameEntity> GetComponent(IEntityComponent<IGameEntity> Component)
        {
            foreach (var item in Components)
            {
                if (item == Component)
                    return item;
            }
            return null;
        }

        public bool RemoveComponent(IEntityComponent<IGameEntity> component)
        {
            return Components.Remove(component);
        }
    }
    public class StandartComponent : IEntityComponent<IGameEntity>
    {
        public IGameEntity Parent { get; set; } = null;

        public bool Activated { get; protected set; }
        #region Events
        virtual public event Action<IEntityComponent<IGameEntity>> OnActivated;
        virtual public event Action<IEntityComponent<IGameEntity>> OnDeactevated;
        virtual public event Action<IEntityComponent<IGameEntity>> OnReset;
        virtual public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        virtual public void WayToAtivate(IGameEntity parrent) { }
        virtual public void WayToDeativate(IGameEntity parrent) { }

        public void Activate(IGameEntity parrent)
        {
            Parent = parrent;
            WayToAtivate(parrent);
            OnActivated?.Invoke(this);
        }
        public void Deactivate(IGameEntity parrent)
        {
            WayToDeativate(parrent);
            OnDeactevated?.Invoke(this);
            Parent = null;
        }
        virtual public void Reset()
        {
            Deactivate(Parent);
        }
    }
}

//namespace Tanks1990MG_csharp.Application.GameEntityes.Test
//{







//    //public class GameEntity : IGameEntity
//    //{
//    //    #region Data
//    //    public Lazy<string> TAG { get; set; } = new Lazy<string>();
//    //    public Lazy<int> GUID { get; set; } = new Lazy<int>();
//    //    public List<IEntityComponent<IGameEntity>> Components { get; } = new List<IEntityComponent<IGameEntity>>();
//    //    public ComponentsSignature ComponentsTypeSignature { get { return new ComponentsSignature(Components); } }
//    //    public List<IGameEntity> ChildEntities { get; }
//    //    public IGameEntity ParentEntity { get; set; }
//    //    #endregion
//    //    #region Events
//    //    public event Action<object, IEntityComponent<IGameEntity>> OnAddComponent;
//    //    public event Action<object, IEntityComponent<IGameEntity>> OnRemoveComponent;
//    //    public event Action<object, IGameEntity> OnChildAdded;
//    //    public event Action<object, IGameEntity> OnChildRemoved;
//    //    #endregion
//    //    #region Child
//    //    public IGameEntity AddChild(IGameEntity entity)
//    //    {
//    //        ChildEntities.Add(entity);
//    //        OnChildAdded?.Invoke(this, entity);
//    //        return this;
//    //    }
//    //    public IGameEntity GetChild(Func<IGameEntity, bool> Filter)
//    //    {
//    //        foreach (var item in ChildEntities)
//    //        {
//    //            if (Filter(item))
//    //                return item;
//    //        }
//    //        return null;
//    //    }

//    //    public IGameEntity RemoveChild(IGameEntity entity)
//    //    {
//    //        ChildEntities.Remove(entity);
//    //        return this;
//    //    }
//    //    #endregion
//    //    #region Components
//    //    public bool AddComponent(IEntityComponent<IGameEntity> component)
//    //    {
//    //        if (GetComponent(component) != null)
//    //            return false;
//    //        component.Activate(this);
//    //        OnAddComponent?.Invoke(this, component);
//    //        return true;
//    //    }
//    //    public T GetComponent<T>() where T : IEntityComponent<IGameEntity>
//    //    {
//    //        foreach (var item in Components)
//    //        {
//    //            if (item is T)
//    //                return (T)item;
//    //        }
//    //        return default;
//    //    }
//    //    public IEntityComponent<IGameEntity> GetComponent(Type ComponentType)
//    //    {
//    //        foreach (var item in Components)
//    //        {
//    //            if (item.GetType() == ComponentType)
//    //                return item;
//    //        }
//    //        return default;
//    //    }
//    //    public IEntityComponent<IGameEntity> GetComponent(IEntityComponent<IGameEntity> Component)
//    //    {
//    //        foreach (var item in Components)
//    //        {
//    //            if (item == Component)
//    //                return item;
//    //        }
//    //        return default;
//    //    }
//    //    public bool RemoveComponent(IEntityComponent<IGameEntity> component)
//    //    {
//    //        if (GetComponent(component) is null)
//    //            return false;
//    //        component.Deactivate(this);
//    //        OnRemoveComponent?.Invoke(this, component);
//    //        return true;
//    //    }
//    //    #endregion
//    //}





//    //public class EntitySystem : ISystem
//    //{
//    //    public List<ISystem> SubSystems { get; } = new List<ISystem>();
//    //    public bool Enabled { get; set; }
//    //    public int UpdateOrder { get; set; }

//    //    public event EventHandler<EventArgs> EnabledChanged;
//    //    public event EventHandler<EventArgs> UpdateOrderChanged;

//    //    private static EntitySystem _Instance;
//    //    public static EntitySystem Instanse { get { if (_Instance is null) _Instance = new EntitySystem(); return _Instance; } }
//    //    public void Update(GameTime gameTime)
//    //    {
//    //        SubSystems.ForEach(i => i.Update(gameTime));
//    //    }
//    //}

//    //class TestComponent : IEntityComponent<IGameEntity>
//    //{
//    //    public IGameEntity Parent => throw new NotImplementedException();

//    //    public bool Activated => throw new NotImplementedException();

//    //    public bool Enabled => throw new NotImplementedException();

//    //    public int UpdateOrder => throw new NotImplementedException();

//    //    public event Action<IEntityComponent<IGameEntity>> OnTryActivate;
//    //    public event Action<IEntityComponent<IGameEntity>> OnActivated;
//    //    public event Action<IEntityComponent<IGameEntity>> OnTryDeactevated;
//    //    public event Action<IEntityComponent<IGameEntity>> OnDeactevated;
//    //    public event Action<IEntityComponent<IGameEntity>> OnReset;
//    //    public event EventHandler<EventArgs> EnabledChanged;
//    //    public event EventHandler<EventArgs> UpdateOrderChanged;
//    //    public event PropertyChangedEventHandler PropertyChanged;

//    //    public void Activate(IGameEntity parrent)
//    //    {
//    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("alpha"));
//    //        throw new NotImplementedException();
//    //    }

//    //    public void Deactivate(IGameEntity parrent)
//    //    {
//    //        throw new NotImplementedException();
//    //    }

//    //    public void Reset()
//    //    {
//    //        throw new NotImplementedException();
//    //    }

//    //    public void Update(GameTime gameTime)
//    //    {
//    //        throw new NotImplementedException();
//    //    }
//    //}
//    /*
//     Контроллер сущностех хранит их список,
//     при добавлении или удалении рассылает событие с сущностью
//     */
//}
