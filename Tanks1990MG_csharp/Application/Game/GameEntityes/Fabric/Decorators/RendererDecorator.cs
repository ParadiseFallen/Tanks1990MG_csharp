using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMCS.Interfaces.Entity;
using Microsoft.Xna.Framework.Graphics;
using Tanks1990MG_csharp.Application.GameEntityes.Components;
using Tanks1990MG_csharp.Application.GameEntityes.Solutions;
using Tanks1990MG_csharp.Application.GameEntityes.Solutions.Fabric.Builders;
using Tanks1990MG_csharp.Application.Interfaces;

namespace Tanks1990MG_csharp.Application.Game.GameEntityes.Fabric.Decorators
{
    class RendererDecorator : IEntityDecorator
    {
        public EntityBuilder Builder { get ; set ; }

        public void Decorate(IGameEntity entity)
        {
            entity.Components.AddComponent(new StaticRendererComponentModel() { Drawable = new Sprite() { Texture =  Builder.Content.Load<Texture2D>("Tank")} }); ;
        }
    }
}
