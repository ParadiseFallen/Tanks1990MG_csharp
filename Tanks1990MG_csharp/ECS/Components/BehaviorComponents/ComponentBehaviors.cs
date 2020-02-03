using EMCS.Interfaces.Components.ComponetsBehavior;
using EMCS.Interfaces.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace EMCS.Interfaces.Components.ComponetsBehavior
{
    public interface IComponentsBehavior : IEntityComponent<IEntity>
    {
        void Update(GameTime time,Dictionary<string,object> ExtraData);
    }
}

namespace EMCS.Realisations.Components.ComponetsBehavior
{
    public class ComponentsBehavior : Component, IComponentsBehavior
    {
        virtual public void Update(GameTime time, Dictionary<string, object> ExtraData)
        {
        }
    }
}