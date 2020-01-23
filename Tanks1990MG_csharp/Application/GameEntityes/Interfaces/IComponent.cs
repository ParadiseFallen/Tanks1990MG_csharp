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
        //каждый компонент имеет родителя
        T Parent{ get; set; }
        //был ли активирован
        bool Activated { get; set; }
        void Activate(T parrent);
        void Deactivate(T parrent);
    }

    public interface IComponentsContainer {
        ObservableCollection<IEntityComponent<IComponentsContainer>> Components { get;}
        T GetComponent<T>() where T : IEntityComponent<IComponentsContainer>;
        IEntityComponent<IComponentsContainer> GetComponent(Type ComponentType);
        IEntityComponent<IComponentsContainer> GetComponent(IEntityComponent<IComponentsContainer> Component);
        bool AddComponent(IEntityComponent<IComponentsContainer> component);
        bool RemoveComponent(IEntityComponent<IComponentsContainer> component);
    }

    public class GameEntity : IComponentsContainer, IUpdateable
    {
        public int GUID { get; set; }
        public Lazy<string> Name { get; set; }
        public GameEntity(params IEntityComponent<IComponentsContainer>[] components)
        {
            for (int i = 0; i < components.Length; i++)
            {
                AddComponent(components[i]);
            }
        }
        public ObservableCollection<IEntityComponent<IComponentsContainer>> Components { get; } = new ObservableCollection<IEntityComponent<IComponentsContainer>>();

        public bool Enabled { get; set; }
        public int UpdateOrder { get; set; }

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public T GetComponent<T>() where T: IEntityComponent<IComponentsContainer>
        {
            foreach (var item in Components.ToList())
            {
                if (item is T)
                    return (T)item;
            }
            return default;
        }
        public IEntityComponent<IComponentsContainer> GetComponent(Type ComponentType)
        {
            foreach (var item in Components.ToList())
            {
                if (item.GetType() == ComponentType)
                    return item;
            }
            return default;
        }
        public IEntityComponent<IComponentsContainer> GetComponent(IEntityComponent<IComponentsContainer> Component)
        {
            foreach (var item in Components.ToList())
            {
                if (item == Component)
                    return item;
            }
            return default;
        }
        public bool AddComponent(IEntityComponent<IComponentsContainer> component)
        {
            if (GetComponent(component) != null)
                return false;
            try
            {
                component.Activate(this);
            }
            catch (Exception)
            {
                return false;
            }
            Components.Add(component);
            component.Parent = this;
            component.Activated = true;
            return true;
        }
        public bool RemoveComponent(IEntityComponent<IComponentsContainer> component)
        {
            try
            {
                component.Deactivate(this);
            }
            catch (Exception)
            {
                return false;
            }

            Components.Remove(component);
            return true;
        }

        public void Update(GameTime gameTime)
        {
            Components.ToList().ForEach(i=>i.Update(gameTime));
        }
    }

}
