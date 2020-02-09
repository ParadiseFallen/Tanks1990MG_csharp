using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Systems.MainSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tanks1990MG_csharp.Application.ECS.Systems;
using Tanks1990MG_csharp.Application.InputMG.Interfaces;
using Tanks1990MG_csharp.Application.States.Interfaces;

namespace Tanks1990MG_csharp.Application.States.States
{
    class GameState : IAppState
    {
        public bool Initialized { get; set; }
        public bool DontUnloadFromMemory { get; set; }
        public List<IBindebleKey> StateKeyboardLayout { get; set; }
        //public Panel GUI { get; set ; }


        public event Action<StateBuilder.StateID> ChangeStateRequest;


        EntitySystemMONOGAME entitySystem = new EntitySystemMONOGAME();

        public GameState()
        {
            entitySystem.Systems.AddSystem(new PhisycSystem());
            entitySystem.Systems.AddSystem(new ECS.Systems.RenderSystem());
            var colisionSystem = new ColisionSystem2D();
            colisionSystem.DeleteEntityColision += (e) => { entitySystem.Entities.RemoveChild(e); };
            entitySystem.Systems.AddSystem(colisionSystem);



            //EntitySystem.Systems.AddSystem();
            /*Build all entityes*/


        }

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
