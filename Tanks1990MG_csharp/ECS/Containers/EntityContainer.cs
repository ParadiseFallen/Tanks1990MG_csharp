using EMCS.Interfaces;
using EMCS.Interfaces.Entity;
using System;
using System.Collections.Generic;

namespace EMCS.Realisations.Containers
{
    public class EntityContainer : IChildContainer<IEntity>
    {
        public List<IEntity> ChildEntities { get; } = new List<IEntity>();
        public IEntity ParentEntity { get; set; } = null;

        public event Action<object, IEntity> OnChildAdded;
        public event Action<object, IEntity> OnChildRemoved;

        public void AddChild(IEntity entity)
        {
            ChildEntities.Add(entity);
            OnChildAdded?.Invoke(this, entity);
        }
        public IEntity GetChild(Func<IEntity, bool> Filter)
        {
            foreach (var item in ChildEntities)
            {
                if (Filter(item))
                    return item;
            }
            return default;
        }
        public IEntity RecursiveGetChild(Func<IEntity, bool> Filter, int depth = 10)
        {

            if (Filter(this.ParentEntity))
                return ParentEntity;

            if (depth < 0)
                return default;

            IEntity entity = null;

            foreach (var item in ChildEntities)
            {
                if (Filter(item))
                    return item;
            }

            foreach (var item in ChildEntities)
            {
                entity = item.Childs.RecursiveGetChild(Filter, depth--);
                if (entity != null)
                    return entity;
            }

            return default;
        }
        public bool RemoveChild(IEntity entity)
        {
            OnChildRemoved?.Invoke(this, entity);
            return ChildEntities.Remove(entity);
        }
    }
}
