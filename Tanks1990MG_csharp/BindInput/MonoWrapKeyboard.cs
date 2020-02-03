using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.InputMG.Interfaces;

namespace Tanks1990MG_csharp.Application.InputMG
{
    class MonoWrapKeyboard : GameComponent
    {
        private IBindebleInputDevice BindebleInputDevice;
        public MonoWrapKeyboard(Microsoft.Xna.Framework.Game game, IBindebleInputDevice bindebleInputDevice) : base(game)
        {
            BindebleInputDevice = bindebleInputDevice;
        }

        public override void Update(GameTime gameTime)
        {
            BindebleInputDevice.Update(gameTime);
            base.Update(gameTime);
        }
    }
}
