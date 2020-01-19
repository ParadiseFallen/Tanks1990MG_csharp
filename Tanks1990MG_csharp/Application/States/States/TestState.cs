using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using Tanks1990MG_csharp.Application.GameEntityes;
using Tanks1990MG_csharp.Application.InputMG.Interfaces;
using Tanks1990MG_csharp.Application.InputMG.Solutions;
using Tanks1990MG_csharp.Application.States.Interfaces;

namespace Tanks1990MG_csharp.Application.States.Solution
{
    class TestState : IAppState
    {
        public bool Initialized { get; set; }
        public bool DontUnloadFromMemory { get; set; } = true;
        public List<IBindebleKey> StateKeyboardLayout { get; set; } = new List<IBindebleKey>();

        public event Action<StateBuilder.StateID> ChangeStateRequest;

        public EntityController EntityController = new EntityController();
        private GraphicController GraphicController = new GraphicController();

        public TestState()
        {
            EntityController.Entities.CollectionChanged += GraphicController.UpdateCollection;
            //StateKeyboardLayout.Add(new BindibleKey("TEST_STATE_M", () => { return Keyboard.GetState().IsKeyDown(Keys.M); }, () => { ChangeStateRequest?.Invoke(StateBuilder.StateID.MainMenu); }));
            //StateKeyboardLayout.Add(new BindibleKey("TEST_STATE_A", () => { return Keyboard.GetState().IsKeyDown(Keys.A); }, () => { Console.WriteLine("Work"); }));
        }

        public void Draw(GraphicsDevice device)
        {
            GraphicController.Draw(device);
        }

        public void Load()
        {
        }

        public void Save()
        {

        }

        public void Update(GameTime time)
        {
            EntityController.Update(time);
        }
    }
}
