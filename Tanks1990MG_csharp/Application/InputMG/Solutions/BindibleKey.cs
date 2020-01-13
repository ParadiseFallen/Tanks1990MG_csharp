using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.InputMG.Interfaces;

namespace Tanks1990MG_csharp.Application.InputMG.Solutions
{
    /// <summary>
    /// Bindible key
    /// </summary>
    public class BindibleKey : IBindebleKey
    {
        #region Data
        public string Description { get; set; }
        public bool Locked { get; set; }
        public Func<bool> Triger { get; set; }
        public event Action Trigered;
        #endregion

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="ActivationFunction">Function of activation</param>
        /// <param name="Fucntion">Action</param>
        /// <param name="Description">Description/param>
        public BindibleKey(string Description, Func<bool> ActivationFunction = null, Action Fucntion = null)
        {
            this.Locked = false;
            this.Triger = ActivationFunction;
            this.Trigered += Fucntion;
            this.Description = Description;
        }
        /// <summary>
        /// Try to trigger
        /// </summary>
        public void Update()
        {
            if (Locked) return;
            if (Triger.Invoke()) Trigered.Invoke();
        }
        /// <summary>
        /// ToString 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"KEY: {Description}\n\tTriger on: {Triger}. Invoke -> {Trigered}";
        }
    }
}
