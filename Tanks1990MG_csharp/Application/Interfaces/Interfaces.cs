using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tanks1990MG_csharp.Application.Interfaces
{
    public interface IUpdatebleTime<T>
    {
        void Update(GameTime time, T arg);
    }
    public interface IUpdatebleTime
    {
        void Update(GameTime time);
    }
    public interface IUpdateble<T>
    {
        void Update(T arg);
    }
    public interface IUpdateble
    {
        void Update();
    }
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch);
    }
    public interface IMoveble<T> : IUpdatebleTime
    {
        T Acceleration { get; set; }
        T Position { get; set; }
        T Rotation { get; set; }
        //sender/arg
        event Action<object, T> AccelerationChanged;
        event Action<object, T> PositionChanged;
        event Action<object, T> RotationChanged;
    }
    //py
    public interface IColision2D
    {
        /// <summary>
        /// Область обьекта(размеры)
        /// </summary>
        Rectangle ColisionArea { get; set; }
        /// <summary>
        /// Вызываеться при столкновении
        /// </summary>
        event Action<IColision2D> Colisioned;
    }
    //py
    public interface IRendererModel : IDrawable, IUpdatebleTime
    {
        IDrawable Source { get; set; }
        event Action<object, IRendererModel> IRendererModelChanged;
    }
}
