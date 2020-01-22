using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.UI;
using Tanks1990MG_csharp.Application.InputMG.Interfaces;
using Tanks1990MG_csharp.Application.States.Interfaces;

namespace Tanks1990MG_csharp.Application.States.States
{
    class LoadingScreen : IAppState
    {
        public bool Initialized { get; set; } = false;
        public bool DontUnloadFromMemory { get; set; } = true;
        public List<IBindebleKey> StateKeyboardLayout { get; set; } = null;
        public Panel GUI { get; set; } = new Panel();

        public event Action<StateBuilder.StateID> ChangeStateRequest;

        public void Draw(GraphicsDevice graphicsDevice)
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
