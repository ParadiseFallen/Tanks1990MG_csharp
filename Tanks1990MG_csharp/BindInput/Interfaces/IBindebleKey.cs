using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.Interfaces;

namespace Tanks1990MG_csharp.Application.InputMG.Interfaces
{
    public interface IBindebleKey : IUpdatebleTime
    {
        /// <summary>
        /// Description of key
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// What key to do when trigered
        /// </summary>
        event Action Trigered;
        /// <summary>
        /// Function of activation key
        /// </summary>
        Func<bool> Triger { get; set; }
        bool Locked { get; set; }
        bool RepeatDelayEnabled { get; set; }
        int RepeatDelayMS { get; set; }
        //TimeSpan LasTimeInvokation { get;}
    }
}
