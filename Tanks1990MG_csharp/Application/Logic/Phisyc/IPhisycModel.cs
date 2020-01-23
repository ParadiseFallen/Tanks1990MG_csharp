using Microsoft.Xna.Framework;
using System;
using Tanks1990MG_csharp.Application.GameEntityes.Interfaces;
using Tanks1990MG_csharp.Application.Interfaces;

namespace Tanks1990MG_csharp.Application.Logic.Phisyc
{
    public interface IPhisycModel : IMoveble<Vector3>, IUpdatebleTime/*,IComponent<IGameEntity>*/
    {
        int AccelerationAttenuationRate0_100 { get; set; }
        IColision MyColison { get; set; }
        void Accelerate(Vector3 acceleration);
        /// <summary>
        /// call when smth changed in model
        /// </summary>
        event Action<IPhisycModel> IPhisycModelUpdated;
    }
}

