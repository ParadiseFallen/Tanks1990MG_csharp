using Microsoft.Xna.Framework;
using System;

namespace EMCS.Interfaces.Components.Model
{
    public interface IModelBehavior
    {
        IEntityComponentModel Model { get; set; }
        void Update(GameTime time);
    }
}
