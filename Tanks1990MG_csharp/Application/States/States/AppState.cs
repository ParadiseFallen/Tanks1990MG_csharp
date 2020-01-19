using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tanks1990MG_csharp.Application.InputMG.Interfaces;
using Tanks1990MG_csharp.Application.States.Interfaces;

namespace Tanks1990MG_csharp.Application.States.Solution
{
    class AppState : IAppState
    {
        public bool Initialized { get; set; } = false;
        public bool DontUnloadFromMemory { get; set; } = false;
        public List<IBindebleKey> StateKeyboardLayout { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<StateBuilder.StateID> ChangeStateRequest;

        public void Draw(GraphicsDevice spriteBatch)
        {

        }

        public void Load()
        {

        }

        public void Save()
        {

        }

        public void Update(GameTime time)
        {

        }
    }
}
