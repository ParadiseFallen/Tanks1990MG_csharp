using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.Interfaces;

namespace Tanks1990MG_csharp.Application.Logic.Phisyc
{
    public interface IPhisycModel : IMoveble<Vector2>, IUpdatebleTime
    {

        int AccelerationAttenuationRate0_100 { get; set; }

        IColision2D MyColison { get; set; }

        void Accelerate(Vector2 acceleration);
        /// <summary>
        /// call when smth changed in model
        /// </summary>
        event Action<IPhisycModel> IPhisycModelUpdated;
    }
}

