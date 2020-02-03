using EMCS.Interfaces;
using EMCS.Interfaces.Entity;
using EMCS.Interfaces.System;
using EMCS.Realisations.Signatures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;



/*

     */


/*
 Модель : rjyntqyth rjvgjytynjd c gjdtltybtv
     */
     /*Разложить и улучшить*/
//namespace EMCS.Interfaces.System
//{
//    //public interface ISystemBehavior
//    //{ 
//    //    Action<IEntityComponent<IEntity>,GameTime> UpdateComponentDefault { get; set; }
//    //    Action<IEntityComponent<IEntity>,GameTime> UpdateModel { get; set; }
//    //}
    
//}


///**/



namespace EMCS.Realisations.Components.Model
{
    //public class StandartModelBehavior : IComponentBehavior
    //{
    //    public IEntityComponentModel Model { get ; set ; }

    //    virtual public void Update(GameTime time) { }
    //}

    //public class StandartModel : IEntityComponentModel
    //{
    //    private IComponentBehavior _Behavior;
    //    public IComponentBehavior Behavior { get { return _Behavior; } set { _Behavior = value; IsBehaviorSet = _Behavior is null ?  false : true;  } }

    //    public bool IsBehaviorSet { get; set; }

    //    public IEntity Parent { get; set; }

    //    public bool Activated { get; protected set; }

    //    public List<IEntityComponent<IEntity>> Components => throw new NotImplementedException();

    //    public ComponentsSignature ComponentsTypeSignature => throw new NotImplementedException();

    //    IEntity IComponentsContainer.Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    //    virtual public event Action<IEntityComponent<IEntity>> OnActivated;
    //    virtual public event Action<IEntityComponent<IEntity>> OnDeactevated;
    //    virtual public event Action<IEntityComponent<IEntity>> OnReset;

    //    virtual public event PropertyChangedEventHandler PropertyChanged;

    //    virtual public event Action<object, IEntityComponent<IEntity>> OnAddComponent;
    //    virtual public event Action<object, IEntityComponent<IEntity>> OnRemoveComponent;

    //    virtual public void WayToAtivate(IEntity parrent) { }
    //    virtual public void WayToDeativate(IEntity parrent) { }

    //    public void Activate(IEntity parrent)
    //    {
    //        Activated = true;
    //        foreach (var item in Components)
    //        {
    //            if (!item.Activated)
    //            {
    //                item.Activate(Parent);
    //                Activated = false;
    //            }
    //        }
    //        WayToAtivate(parrent);
    //    }
        
    //    public bool AddComponent(IEntityComponent<IEntity> component)
    //    {
    //        if (Components.Contains(component))
    //            return false;
    //        component.Activate(Parent);
    //        Components.Add(component);
    //        return true;
    //    }

    //    public void Deactivate(IEntity parrent)
    //    {
    //        foreach (var item in Components)
    //        {
    //            item.Deactivate(Parent);
    //        }
    //        Activated = false;
    //        WayToDeativate(parrent);
    //    }

    //    public IEntityComponent<IEntity> GetComponent(Type ComponentType)
    //    {
    //        foreach (var item in Components)
    //        {
    //            if (item.GetType() == ComponentType)
    //                return item;
    //        }
    //        return default;
    //    }

    //    public IEntityComponent<IEntity> GetComponent(IEntityComponent<IEntity> Component)
    //    {
    //        foreach (var item in Components)
    //        {
    //            if (item == Component)
    //                return item;
    //        }
    //        return default;
    //    }

    //    public bool RemoveComponent(IEntityComponent<IEntity> component)
    //    {
    //        return Components.Remove(component);
    //    }

    //    virtual public void Reset()
    //    {
    //    }

    //    public T GetComponent<T>() where T : class, IEntityComponent<IEntity>
    //    {
    //        foreach (var item in Components)
    //        {
    //            if (item is T)
    //                return item as T;
    //        }
    //        return default;
    //    }
    //}

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
