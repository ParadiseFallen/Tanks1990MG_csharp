using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tanks1990MG_csharp.Application.InputMG.Interfaces;

namespace Tanks1990MG_csharp.Application.InputMG.Solutions
{
    /// <summary>
    /// Bindible key
    /// </summary>
    public class BindibleKey : IBindebleKey
    {
        #region Data
        private async void Execute(Action action, int timeoutInMilliseconds)
        {
            await Task.Delay(timeoutInMilliseconds);
            action();
        }
        public string Description { get; set; }
        public bool Locked { get; set; }
        public Func<bool> Triger { get; set; }
        public bool RepeatDelayEnabled { get; set; } = false;

        private int _RepeatDelayMS = 0;
        public int RepeatDelayMS { get { return _RepeatDelayMS; } set { RepeatDelayEnabled = true; _RepeatDelayMS = value;  } }
        private bool CanActivate { get; set; } = true;

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
        public void Update(GameTime time)
        {
            if (Locked)
                return;
            if (Triger.Invoke() && CanActivate)
            {
                Trigered.Invoke();
                if (RepeatDelayEnabled)
                {
                    CanActivate = false;
                    Execute(() => CanActivate = true, RepeatDelayMS);
                }
            }
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
