using EMCS.Interfaces;
using EMCS.Interfaces.Components.ComponentsContainer;
using EMCS.Interfaces.Entity;
using EMCS.Realisations.Components.ComponentsContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMCS.Realisations.Entity
{
    public class Entity : IEntity
    {
        #region Props
        public string TAG { get; set; } = "";
        public int GUID { get; set; } = -1;
        //Components
        public IComponentsContainer Components { get; set; } 
        //childs
        public IChildContainer<IEntity> Childs { get; }
        //public List<IEntity> ChildEntities { get; } = new List<IEntity>();
        //parrent
        public IEntity ParentEntity { get; set; } = null;
        #endregion
        #region Events
        public event Action<object, IEntityComponent<IEntity>> OnAddComponent;
        public event Action<object, IEntityComponent<IEntity>> OnRemoveComponent;
        //public event Action<object, IEntity> OnChildAdded;
        //public event Action<object, IEntity> OnChildRemoved;
        #endregion

        public Entity()
        {
            Components.Parent = this;
        }

        #region Methods
        //virtual public void AddChild(IEntity entity)
        //{
        //    ChildEntities.Add(entity);
        //    OnChildAdded?.Invoke(this, entity);
        //}

        //virtual public bool AddComponent(IEntityComponent<IEntity> component)
        //{
        //    var t = Components.AddComponent(component);
        //    Components.
        //    OnAddComponent?.Invoke(this, component);
        //    return t;
        //}

        //virtual public IEntity GetChild(Func<IEntity, bool> Filter)
        //{
        //    foreach (var item in ChildEntities)
        //    {
        //        if (Filter(item))
        //            return item;
        //    }
        //    return default;
        //}

        //virtual public T GetComponent<T>() where T : class, IEntityComponent<IEntity>
        //{
        //    return Components.GetComponent<T>();
        //}

        //virtual public IEntityComponent<IEntity> GetComponent(Type ComponentType)
        //{
        //    return Components.GetComponent(ComponentType);
        //}

        //virtual public IEntityComponent<IEntity> GetComponent(IEntityComponent<IEntity> Component)
        //{
        //    return Components.GetComponent(Component);
        //}

        //virtual public IEntity RecursiveGetChild(Func<IEntity, bool> Filter, int depth = 10)
        //{
        //    IEntity res = null;

        //    if (depth < 0)
        //        return null;
        //    foreach (var item in ChildEntities)
        //    {
        //        if (Filter(item))
        //            return item;

        //        res = item.RecursiveGetChild(Filter, depth--);
        //        if (res != null)
        //            return res;
        //    }
        //    return res;
        //}

        //virtual public bool RemoveChild(IEntity entity)
        //{
        //    return ChildEntities.Remove(entity);
        //}

        //public bool RemoveComponent(IEntityComponent<IEntity> component)
        //{
        //    return Components.RemoveComponent(component);
        //}

        #endregion
    }
}
