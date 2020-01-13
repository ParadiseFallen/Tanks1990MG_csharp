using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990MG_csharp.Application.InputMG.Interfaces
{
    public interface IBindebleKey
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
        void Update();
        bool Locked { get; set; }

    }
}
