using EMCS.Interfaces.Entity;
using EMCS.Realisations.Signatures;
using System;
using System.Collections.Generic;

namespace EMCS.Interfaces.Components.Component
{
    public interface IComponentsContainer
    {
        IGameEntity Parent { get; set; }
        //список компонентов. Можно сделать приватным
        List<IEntityComponent<IGameEntity>> Components { get; }
        //получить уже типизированный компонент по его сигнатуре
        T GetComponent<T>() where T :class, IEntityComponent<IGameEntity>;
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
