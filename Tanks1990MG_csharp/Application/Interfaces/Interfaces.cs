using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Tanks1990MG_csharp.Application.GameEntityes.Interfaces;
using Tanks1990MG_csharp.Application.Logic.Phisyc;

namespace Tanks1990MG_csharp.Application.Interfaces
{
    //public interface IUpdatebleTime<T>
    //{
    //    void Update(GameTime time, T arg);
    //}
    public interface IUpdatebleTime
    {
        void Update(GameTime time);
    }
    //public interface IUpdateble<T>
    //{
    //    void Update(T arg);
    //}
    //public interface IUpdateble
    //{
    //    void Update();
    //}

    public interface IDrawable
    {
        void Draw(GraphicsDevice graphicsDevice);
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

    public interface IColision : IUpdateable {
        IPhisycModel ParentPhisycModel { get; set; }
        event Action<IColision> OnColisionEnter;
        event Action<IColision> OnColisionExit;
        event Action<IColision> OnColisionStay;
    }
    //py
    public interface IColision2D : IColision
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
    public interface IRendererModel : IDrawable, IUpdatebleTime, IComponent<IGameEntity>
    {
        IDrawable Source { get; set; }
        event Action<object, IRendererModel> IRendererModelChanged;
    }
}
