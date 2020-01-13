using System;

namespace Tanks1990MG_csharp.Application.GameEntityes.Interfaces
{
    //public interface IAction 
    //{
    //    void Execute(IGameEntity entity,EventArgs args);
    //}
    
    public interface IControllerModel
    {
        IGameEntity MyEntity { get; set; }
    }
}