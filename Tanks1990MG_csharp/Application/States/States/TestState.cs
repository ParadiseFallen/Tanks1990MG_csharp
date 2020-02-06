using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Fabric;
using ECS.Systems.MainSystem;
using EMCS.Interfaces.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tanks1990MG_csharp.Application.ECS.Components;
using Tanks1990MG_csharp.Application.ECS.Fabric.Decorators;
using Tanks1990MG_csharp.Application.ECS.Systems;
using Tanks1990MG_csharp.Application.GameEntityes;
using Tanks1990MG_csharp.Application.InputMG.Interfaces;
using Tanks1990MG_csharp.Application.InputMG.Solutions;
using Tanks1990MG_csharp.Application.States.Interfaces;

namespace Tanks1990MG_csharp.Application.States.Solution
{
    class Controller
    {
        public IEntity Target { set { phisycComponent = value.Components.GetComponent<PhisycComponent>(); } }
        private PhisycComponent phisycComponent;
        public void MoveEntity(Vector3 Acc)
        {
            phisycComponent.Acceleration += Acc;
        }
    }
    class TestState : IAppState
    {
        public bool Initialized { get; set; }
        public bool DontUnloadFromMemory { get; set; } = true;
        public List<IBindebleKey> StateKeyboardLayout { get; set; } = new List<IBindebleKey>();

        public event Action<StateBuilder.StateID> ChangeStateRequest;
        Controller controller = new Controller();
        //public EntityController EntityController = new EntityController();
        IEntity ent;
        EntitySystemMONOGAME entitySystem = new EntitySystemMONOGAME();
        
        
        //Func<GameTime,bool> Try;
        /*Это работает!!!*/
        //Try = CSScriptLibrary.CSScript.Evaluator.LoadDelegate<Func<GameTime,bool>>(@"
        //    using Microsoft.Xna.Framework;
        //    using Microsoft.Xna.Framework.Input;
        //    bool any(GameTime time)
        //    {
        //        return Keyboard.GetState().IsKeyDown(Keys.A)&&Keyboard.GetState().IsKeyDown(Keys.B);
        //    }
        //    ");
        public TestState()
        {
            EntityBuilder.BuilderInstance.Decorators.Add("PhisycModelDecorator", new PhisycModelDecorator());
            //var t = EntityBuilder.BuilderInstance.GetWrap().StartBuild("CustomEntity").Decorate("TankTextureSprite").Resault;
            
            
            entitySystem.Systems.AddSystem(new PhisycSystem());
            

            //EntityController.Entities.CollectionChanged += GraphicController.UpdateCollection;
            ent = EntityBuilder.BuilderInstance.GetWrap().StartBuild("CustomEntity").Decorate("PhisycModelDecorator").Resault;
            controller.Target = ent;
            entitySystem.Entities.AddChild(ent);

            //ent.Components.GetComponent<PhisycComponent>().PropertyChanged += (s, a) => { Console.WriteLine($"Position : {(s as PhisycComponent).Position}"); };
            

            

            /*Control entity*/
            StateKeyboardLayout.Add(new BindibleKey("TEST_STATE_W", () => { return Keyboard.GetState().IsKeyDown(Keys.W); }, () => { controller.MoveEntity(new Vector3( 0,  100, 0)); }));
            StateKeyboardLayout.Add(new BindibleKey("TEST_STATE_A", () => { return Keyboard.GetState().IsKeyDown(Keys.A); }, () => { controller.MoveEntity(new Vector3(-100,  0, 0)); }));
            StateKeyboardLayout.Add(new BindibleKey("TEST_STATE_S", () => { return Keyboard.GetState().IsKeyDown(Keys.S); }, () => { controller.MoveEntity(new Vector3( 0, -100, 0)); }));
            StateKeyboardLayout.Add(new BindibleKey("TEST_STATE_D", () => { return Keyboard.GetState().IsKeyDown(Keys.D); }, () => { controller.MoveEntity(new Vector3( 100,  0, 0)); }));
        }

        public void Load()
        {
        }

        public void Save()
        {

        }

        public void Update(GameTime time)
        {
            entitySystem.Update(time);
            //EntityController.Update(time);
        }
    }
}
