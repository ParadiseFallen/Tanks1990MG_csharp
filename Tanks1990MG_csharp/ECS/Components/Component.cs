using EMCS.Interfaces;
using EMCS.Interfaces.Entity;
using System;
using System.ComponentModel;

namespace EMCS.Realisations.Components
{
    public class Component : IEntityComponent<IEntity>
    {
        public IEntity Parent { get; set; } = null;

        public bool IsActivated { get; protected set; }
        public bool IsPartOfBehavior { get ; set ; }
        #region Events
        virtual public event Action<IEntityComponent<IEntity>> OnActivated;
        virtual public event Action<IEntityComponent<IEntity>> OnDeactevated;
        virtual public event Action<IEntityComponent<IEntity>> OnReset;
        virtual public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        virtual public void WayToAtivate(IEntity parrent) { }
        virtual public void WayToDeativate(IEntity parrent) { }

        public void Activate(IEntity parrent)
        {
            Parent = parrent;
            WayToAtivate(parrent);
            OnActivated?.Invoke(this);
        }
        public void Deactivate(IEntity parrent)
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