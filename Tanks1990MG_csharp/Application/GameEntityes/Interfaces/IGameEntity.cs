using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.Interfaces;
using Tanks1990MG_csharp.Application.Logic.Phisyc;

namespace Tanks1990MG_csharp.Application.GameEntityes.Interfaces
{
    [Obsolete("IGameEntity now is componenteble")]
    public interface IGameEntity : IUpdatebleTime, Application.Interfaces.IDrawable
    {
        uint GUID { get; set; }
        IPhisycModel PhisycModel { get; set; }
        IRendererModel RendererModel { get; set; }
        IControllerModel ControllerModel { get; set; }
        string Name { get; set; }

        event Action<object, IControllerModel> OnControllerChanged;
        event Action<object, IPhisycModel> OnPhisycModelChanged;
        event Action<object, IRendererModel> OnRendererModelChanged;
    }
}
