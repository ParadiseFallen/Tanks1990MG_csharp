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



namespace EMCS.Interfaces
{
    public interface IChildContainer<T>
    {
        //у сущности могут быть наследники
        List<T> ChildEntities { get; }
        void AddChild(T entity);
        bool RemoveChild(T entity);
        T GetChild(Func<T, bool> Filter);
        T RecursiveGetChild(Func<T, bool> Filter,int depth = 10);

        event Action<object, T> OnChildAdded;
        event Action<object, T> OnChildRemoved;
    }
    public interface IEntityComponent<T> : IUpdateable, INotifyPropertyChanged
    {
        #region Data
        //ссылка на родителя
        T Parent { get; }
        //был ли активирован правильно, или еще требует активации
        bool Activated { get; }
        #endregion

        #region Methods
        //Попытка активации
        void Activate(T parrent);
        //попытка деактивации
        void Deactivate(T parrent);
        //сброс данных, для переиспользования компонента
        void Reset();
        #endregion

        #region Events
        //срабатывает при старте активации
        event Action<IEntityComponent<T>> OnTryActivate;
        //срабатывает когда активировалось успешно
        event Action<IEntityComponent<T>> OnActivated;
        //срабатывает при старте деактивации
        event Action<IEntityComponent<T>> OnTryDeactevated;
        //срабатывает при успешной деактивации
        event Action<IEntityComponent<T>> OnDeactevated;
        //срабатывает при сбросе
        event Action<IEntityComponent<T>> OnReset;
        #endregion
    }
    public interface IHaveParent<T>
    {
        T ParentEntity { get; set; }
    }
}
namespace EMCS.Interfaces.Components.Model
{
    public interface IModelBehavior
    {
        Action<GameTime> Update { get; set; }
    }
    public interface IEntityComponentModel : INotifyPropertyChanged, IEntityComponent<IGameEntity>, IComponentsContainer
    {
        IModelBehavior Behavior { get; set; }
        bool IsBehaviorSet { get; }
    }
}
namespace EMCS.Interfaces.Components.Component
{
    public interface IComponentsContainer
    {
        //список компонентов. Можно сделать приватным
        List<IEntityComponent<IGameEntity>> Components { get; }
        //получить уже типизированный компонент по его сигнатуре
        T GetComponent<T>() where T : IEntityComponent<IGameEntity>;
        //получить компонент по сигнатуре
        IEntityComponent<IGameEntity> GetComponent(Type ComponentType);
        //получить компонент по экземпляру
        IEntityComponent<IGameEntity> GetComponent(IEntityComponent<IGameEntity> Component);
        //добавить компонент
        bool AddComponent(IEntityComponent<IGameEntity> component);
        //убрать компонент
        bool RemoveComponent(IEntityComponent<IGameEntity> component);
        //получаем сигнатруру
        ComponentsSignature ComponentsTypeSignature { get; }
        event Action<object, IEntityComponent<IGameEntity>> OnAddComponent;
        event Action<object, IEntityComponent<IGameEntity>> OnRemoveComponent;
    }
}
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
        void AddEntity(ref IGameEntity gameEntity);
        void RemoveEntity(ref IGameEntity gameEntity);
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
    public interface IGameEntity : IComponentsContainer, IChildContainer<IGameEntity>, IHaveParent<IGameEntity>
    {
        //может быть TAG
        Lazy<string> TAG { get; set; }
        //может быть GUID
        Lazy<int> GUID { get; set; }

        void Update(GameTime time);
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
                Entities.OnChildAdded += (Esender,Earg)=> { Sa.AddEntity(ref Earg); };
                //Теперь добавление в Entities будет вызывать удаление из системы
                Entities.OnChildRemoved += (Esender, Earg) => { Sa.RemoveEntity(ref Earg); };
            };
            Systems.OnRemoveSystem += (Ss, Sa) => {
                Entities.OnChildAdded -= (Esender,Earg)=> { Sa.AddEntity(ref Earg); };
                Entities.OnChildRemoved -= (Esender, Earg) => { Sa.RemoveEntity(ref Earg); };
            };

        }

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public void Update(GameTime gameTime)
        {
            Systems.Systems.ForEach(i => i.Update(gameTime));
        }
    }

}
namespace EMCS.Realisations.Components.Model
{
    public class ModelBehavior : IModelBehavior
    {
        public Action<GameTime> Update { get ; set ; }
    }
}
namespace EMCS.Realisations.Components.Component
{
    public class ComponentContainer : IComponentsContainer
    {
        public List<IEntityComponent<IGameEntity>> Components { get; }
        public ComponentsSignature ComponentsTypeSignature { get; }

        public event Action<object, IEntityComponent<IGameEntity>> OnAddComponent;
        public event Action<object, IEntityComponent<IGameEntity>> OnRemoveComponent;

        public bool AddComponent(IEntityComponent<IGameEntity> component)
        {
            if (true)
            {

            }
            throw new NotImplementedException();
        }

        public T GetComponent<T>() where T : IEntityComponent<IGameEntity>
        {
            throw new NotImplementedException();
        }

        public IEntityComponent<IGameEntity> GetComponent(Type ComponentType)
        {
            throw new NotImplementedException();
        }

        public IEntityComponent<IGameEntity> GetComponent(IEntityComponent<IGameEntity> Component)
        {
            throw new NotImplementedException();
        }

        public bool RemoveComponent(IEntityComponent<IGameEntity> component)
        {
            throw new NotImplementedException();
        }
    }
}

namespace Tanks1990MG_csharp.Application.GameEntityes.Test
{







    //public class GameEntity : IGameEntity
    //{
    //    #region Data
    //    public Lazy<string> TAG { get; set; } = new Lazy<string>();
    //    public Lazy<int> GUID { get; set; } = new Lazy<int>();
    //    public List<IEntityComponent<IGameEntity>> Components { get; } = new List<IEntityComponent<IGameEntity>>();
    //    public ComponentsSignature ComponentsTypeSignature { get { return new ComponentsSignature(Components); } }
    //    public List<IGameEntity> ChildEntities { get; }
    //    public IGameEntity ParentEntity { get; set; }
    //    #endregion
    //    #region Events
    //    public event Action<object, IEntityComponent<IGameEntity>> OnAddComponent;
    //    public event Action<object, IEntityComponent<IGameEntity>> OnRemoveComponent;
    //    public event Action<object, IGameEntity> OnChildAdded;
    //    public event Action<object, IGameEntity> OnChildRemoved;
    //    #endregion
    //    #region Child
    //    public IGameEntity AddChild(IGameEntity entity)
    //    {
    //        ChildEntities.Add(entity);
    //        OnChildAdded?.Invoke(this, entity);
    //        return this;
    //    }
    //    public IGameEntity GetChild(Func<IGameEntity, bool> Filter)
    //    {
    //        foreach (var item in ChildEntities)
    //        {
    //            if (Filter(item))
    //                return item;
    //        }
    //        return null;
    //    }

    //    public IGameEntity RemoveChild(IGameEntity entity)
    //    {
    //        ChildEntities.Remove(entity);
    //        return this;
    //    }
    //    #endregion
    //    #region Components
    //    public bool AddComponent(IEntityComponent<IGameEntity> component)
    //    {
    //        if (GetComponent(component) != null)
    //            return false;
    //        component.Activate(this);
    //        OnAddComponent?.Invoke(this, component);
    //        return true;
    //    }
    //    public T GetComponent<T>() where T : IEntityComponent<IGameEntity>
    //    {
    //        foreach (var item in Components)
    //        {
    //            if (item is T)
    //                return (T)item;
    //        }
    //        return default;
    //    }
    //    public IEntityComponent<IGameEntity> GetComponent(Type ComponentType)
    //    {
    //        foreach (var item in Components)
    //        {
    //            if (item.GetType() == ComponentType)
    //                return item;
    //        }
    //        return default;
    //    }
    //    public IEntityComponent<IGameEntity> GetComponent(IEntityComponent<IGameEntity> Component)
    //    {
    //        foreach (var item in Components)
    //        {
    //            if (item == Component)
    //                return item;
    //        }
    //        return default;
    //    }
    //    public bool RemoveComponent(IEntityComponent<IGameEntity> component)
    //    {
    //        if (GetComponent(component) is null)
    //            return false;
    //        component.Deactivate(this);
    //        OnRemoveComponent?.Invoke(this, component);
    //        return true;
    //    }
    //    #endregion
    //}





    //public class EntitySystem : ISystem
    //{
    //    public List<ISystem> SubSystems { get; } = new List<ISystem>();
    //    public bool Enabled { get; set; }
    //    public int UpdateOrder { get; set; }

    //    public event EventHandler<EventArgs> EnabledChanged;
    //    public event EventHandler<EventArgs> UpdateOrderChanged;

    //    private static EntitySystem _Instance;
    //    public static EntitySystem Instanse { get { if (_Instance is null) _Instance = new EntitySystem(); return _Instance; } }
    //    public void Update(GameTime gameTime)
    //    {
    //        SubSystems.ForEach(i => i.Update(gameTime));
    //    }
    //}

    //class TestComponent : IEntityComponent<IGameEntity>
    //{
    //    public IGameEntity Parent => throw new NotImplementedException();

    //    public bool Activated => throw new NotImplementedException();

    //    public bool Enabled => throw new NotImplementedException();

    //    public int UpdateOrder => throw new NotImplementedException();

    //    public event Action<IEntityComponent<IGameEntity>> OnTryActivate;
    //    public event Action<IEntityComponent<IGameEntity>> OnActivated;
    //    public event Action<IEntityComponent<IGameEntity>> OnTryDeactevated;
    //    public event Action<IEntityComponent<IGameEntity>> OnDeactevated;
    //    public event Action<IEntityComponent<IGameEntity>> OnReset;
    //    public event EventHandler<EventArgs> EnabledChanged;
    //    public event EventHandler<EventArgs> UpdateOrderChanged;
    //    public event PropertyChangedEventHandler PropertyChanged;

    //    public void Activate(IGameEntity parrent)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("alpha"));
    //        throw new NotImplementedException();
    //    }

    //    public void Deactivate(IGameEntity parrent)
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
    //}
    /*
     Контроллер сущностех хранит их список,
     при добавлении или удалении рассылает событие с сущностью
     */
}
