using EMCS.Interfaces.Components.ComponentsContainer;

namespace EMCS.Interfaces.Entity
{
    public interface IEntity : /*IChildContainer<IEntity>,*/ IHaveParent<IEntity>
    {
        //может быть TAG
        string TAG { get; set; }
        //может быть GUID
        int GUID { get; set; }
        IComponentsContainer Components { get; }
        IChildContainer<IEntity> Childs { get; }
    }
}
