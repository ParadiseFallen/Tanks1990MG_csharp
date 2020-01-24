using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990MG_csharp.Application.GameEntityes.Test
{

    public interface IEntityComponent<T> : IUpdateable
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

    public interface IComponentsContainer
    {
        //список компонентов. Можно сделать приватным
        List<IEntityComponent<IComponentsContainer>> Components { get; }
        //получить уже типизированный компонент по его сигнатуре
        T GetComponent<T>() where T : IEntityComponent<IComponentsContainer>;
        //получить компонент по сигнатуре
        IEntityComponent<IComponentsContainer> GetComponent(Type ComponentType);
        //получить компонент по экземпляру
        IEntityComponent<IComponentsContainer> GetComponent(IEntityComponent<IComponentsContainer> Component);
        //добавить компонент
        bool AddComponent(IEntityComponent<IComponentsContainer> component);
        //убрать компонент
        bool RemoveComponent(IEntityComponent<IComponentsContainer> component);
        //получаем сигнатруру
        ComponentsSignature ComponentsTypeSignature { get; }
        event Action<object, IEntityComponent<IComponentsContainer>> OnAddComponent;
        event Action<object, IEntityComponent<IComponentsContainer>> OnRemoveComponent;
    }

    public interface IGameEntity : IComponentsContainer
    {
        //у сущности может быть родитель
        Lazy<IGameEntity> ParentEntity { get; set; }
        //у сущности могут быть наследники
        Lazy<List<IGameEntity>> ChildEntities { get; }
        //может быть TAG
        Lazy<string> TAG { get; set; }
        //может быть GUID
        Lazy<string> GUID { get; set; }
    }

    //public class GameEntity : IComponentsContainer
    //{
    //    public int GUID { get; set; }
    //    public Lazy<string> Name { get; set; }
    //    public GameEntity(params IEntityComponent<IComponentsContainer>[] components)
    //    {
    //        for (int i = 0; i < components.Length; i++)
    //        {
    //            AddComponent(components[i]);
    //        }
    //    }
    //    public ObservableCollection<IEntityComponent<IComponentsContainer>> Components { get; } = new ObservableCollection<IEntityComponent<IComponentsContainer>>();

    //    public bool Enabled { get; set; }
    //    public int UpdateOrder { get; set; }

    //    public ComponentsSignature ComponentsTypeSignature => throw new NotImplementedException();

    //    public event EventHandler<EventArgs> EnabledChanged;
    //    public event EventHandler<EventArgs> UpdateOrderChanged;

    //    public T GetComponent<T>() where T : IEntityComponent<IComponentsContainer>
    //    {
    //        foreach (var item in Components.ToList())
    //        {
    //            if (item is T)
    //                return (T)item;
    //        }
    //        return default;
    //    }
    //    public IEntityComponent<IComponentsContainer> GetComponent(Type ComponentType)
    //    {
    //        foreach (var item in Components.ToList())
    //        {
    //            if (item.GetType() == ComponentType)
    //                return item;
    //        }
    //        return default;
    //    }
    //    public IEntityComponent<IComponentsContainer> GetComponent(IEntityComponent<IComponentsContainer> Component)
    //    {
    //        foreach (var item in Components.ToList())
    //        {
    //            if (item == Component)
    //                return item;
    //        }
    //        return default;
    //    }
    //    public bool AddComponent(IEntityComponent<IComponentsContainer> component)
    //    {
    //        if (GetComponent(component) != null)
    //            return false;
    //        try
    //        {
    //            component.Activate(this);
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }
    //        Components.Add(component);

    //        return true;
    //    }
    //    public bool RemoveComponent(IEntityComponent<IComponentsContainer> component)
    //    {
    //        try
    //        {
    //            component.Deactivate(this);
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }

    //        Components.Remove(component);
    //        return true;
    //    }

    //    public void Update(GameTime gameTime)
    //    {
    //        Components.ToList().ForEach(i => i.Update(gameTime));
    //    }
    //    public void ActivateAll(Func<IEntityComponent<IComponentsContainer>, bool> Filter)
    //    {
    //        Components.ToList().ForEach(
    //            i =>
    //            {
    //                if (Filter(i))
    //                    i.Activate(this);
    //            }
    //            );
    //    }
    //}

    /*
     Контроллер сущностех хранит их список,
     при добавлении или удалении рассылает событие с сущностью
     */
}
