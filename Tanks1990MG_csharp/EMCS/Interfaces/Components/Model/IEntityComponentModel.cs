using EMCS.Interfaces.Components.Component;
using EMCS.Interfaces.Entity;
using System.ComponentModel;

namespace EMCS.Interfaces.Components.Model
{
    public interface IEntityComponentModel :  IEntityComponent<IGameEntity>, IComponentsContainer
    {
        IModelBehavior Behavior { get; }
        bool IsBehaviorSet { get; }
    }
}
