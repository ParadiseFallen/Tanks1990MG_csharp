using Microsoft.Xna.Framework;
using System;
using Tanks1990MG_csharp.Application.Interfaces;

namespace Tanks1990MG_csharp.Application.GameEntityes.Interfaces
{
    //public interface IAction 
    //{
    //    void Execute(IGameEntity entity,EventArgs args);
    //}
    
    public interface IControllerModel : IUpdateable
    {
        IGameEntity Entity { get; set; }
    }
}