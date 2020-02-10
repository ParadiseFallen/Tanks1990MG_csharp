using ECS.Fabric;
using ECS.Fabric.Decorators;
using EMCS.Interfaces.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tanks1990MG_csharp.Application.ECS.Components;
using Tanks1990MG_csharp.Application.ECS.Dependencies;

namespace Tanks1990MG_csharp.Application.Game.GameEntityes.Fabric.Decorators
{
    class RendererDecorator : Decorator
    {
        override public void Decorate(IEntity entity)
        {
           entity.Components.AddComponent(new RenderComponent() { Source = new Sprite2D() { Texture =  Builder.Content.Load<Texture2D>("Tank") ,Position = Vector3.Zero,Scale = new Vector3(.5f,.5f,.5f)} }); ;
        }
    }
    class BulletRendererDecorator : Decorator
    {
        override public void Decorate(IEntity entity)
        {
           entity.Components.AddComponent(new RenderComponent() { Source = new Sprite2D() { Texture =  Builder.Content.Load<Texture2D>("Bullet") ,Position = Vector3.Zero,Scale = new Vector3(.5f,.5f,.5f)} }); ;
        }
    }
}
