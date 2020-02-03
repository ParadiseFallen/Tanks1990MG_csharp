using EMCS.Interfaces;
using EMCS.Interfaces.Components.ComponentsContainer;
using EMCS.Interfaces.Entity;
using EMCS.Realisations.Signatures;
using System;
using System.Collections.Generic;

namespace EMCS.Interfaces.Components.ComponentsContainer
{
    public interface IComponentsContainer
    {
        IEntity Parent { get; set; }
        //список компонентов. Можно сделать приватным
        List<IEntityComponent<IEntity>> Components { get; }
        //получить уже типизированный компонент по его сигнатуре
        T GetComponent<T>() where T :class, IEntityComponent<IEntity>;
        //получить компонент по сигнатуре
        IEntityComponent<IEntity> GetComponent(Type ComponentType);
        //получить компонент по экземпляру
        IEntityComponent<IEntity> GetComponent(IEntityComponent<IEntity> Component);
        //добавить компонент
        bool AddComponent(IEntityComponent<IEntity> component);
        //убрать компонент
        bool RemoveComponent(IEntityComponent<IEntity> component);
        //получаем сигнатруру
        ComponentsSignature ComponentsTypeSignature { get; }

        event Action<object, IEntityComponent<IEntity>> OnAddComponent;

        event Action<object, IEntityComponent<IEntity>> OnRemoveComponent;
    }
}

namespace EMCS.Realisations.Components.ComponentsContainer
{
    public class ComponentContainer : IComponentsContainer
    {
        public List<IEntityComponent<IEntity>> Components { get; } = new List<IEntityComponent<IEntity>>();
        public ComponentsSignature ComponentsTypeSignature { get { return new ComponentsSignature(Components); } }
        public IEntity Parent { get; set; } = null;

        public event Action<object, IEntityComponent<IEntity>> OnAddComponent;
        public event Action<object, IEntityComponent<IEntity>> OnRemoveComponent;
        public event Action<object, IEntityComponent<IEntity>> OnSetParent;

        public bool AddComponent(IEntityComponent<IEntity> component)
        {
            if (Components.Contains(component))
                return false;
            Components.Add(component);
            return true;
        }

        public T GetComponent<T>() where T : class, IEntityComponent<IEntity>
        {
            foreach (var item in Components)
            {
                if (item is T)
                    return item as T;
            }
            return null;
        }

        public IEntityComponent<IEntity> GetComponent(Type ComponentType)
        {
            foreach (var item in Components)
            {
                if (item.GetType() == ComponentType)
                    return item;
            }
            return null;
        }

        public IEntityComponent<IEntity> GetComponent(IEntityComponent<IEntity> Component)
        {
            foreach (var item in Components)
            {
                if (item == Component)
                    return item;
            }
            return null;
        }

        public bool RemoveComponent(IEntityComponent<IEntity> component)
        {
            return Components.Remove(component);
        }
    }
}

