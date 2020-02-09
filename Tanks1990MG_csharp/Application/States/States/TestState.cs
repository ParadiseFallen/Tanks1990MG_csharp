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
using Tanks1990MG_csharp.Application.ECS.Dependencies;
using Tanks1990MG_csharp.Application.ECS.Fabric.Decorators;
using Tanks1990MG_csharp.Application.ECS.Systems;
using Tanks1990MG_csharp.Application.Game.GameEntityes.Fabric.Decorators;
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
        private EntityController entityController;
        public TestState()
        {
            //EntityBuilder.BuilderInstance.Decorators.Add("PhisycModelDecorator", new PhisycModelDecorator());
            //EntityBuilder.BuilderInstance.Decorators.Add("TankTextureSprite", new RendererDecorator());

            //var t = EntityBuilder.BuilderInstance.GetWrap().StartBuild("CustomEntity").Decorate("TankTextureSprite").Resault;
            
            
            entitySystem.Systems.AddSystem(new PhisycSystem());
            entitySystem.Systems.AddSystem(new ECS.Systems.RenderSystem());
            entitySystem.Systems.AddSystem(new ColisionSystem2D());

            PrefabList.Instance.Prefabs.Add("tank", new Prefab() { Name = "Tank", Chain = new DecorationChain() { EntityProviderName = "CustomEntity", DecoratorsChain = new List<string>() { "PhisycModelDecorator", "TankTextureSprite" ,"ColisionDecorator"} } });
            //EntityController.Entities.CollectionChanged += GraphicController.UpdateCollection;
            ent = PrefabList.Instance.Construct("tank");
            var ent4 = EntityBuilder.BuilderInstance.GetWrap().StartBuild("CustomEntity").Decorate("PhisycModelDecorator").Decorate("TankTextureSprite").Decorate("ColisionDecorator").Decorate("ControllerDecorator").Resault;
            //controller.Target = ent;
            entitySystem.Entities.AddChild(ent);
            entitySystem.Entities.AddChild(ent4);
             

            ent4.Components.GetComponent<PhisycComponent>().Position += new Vector3(500, 500, 0);
            ent.Components.GetComponent<PhisycComponent>().Position += new Vector3(100, 100, 0);

            //ent.Components.GetComponent<PhisycComponent>().PropertyChanged += (s, a) => { Console.WriteLine($"Position : {(s as PhisycComponent).Position}"); };

            entityController = ent4.Components.GetComponent<EntityController>();
            entityController.OnShot += (controller) => {
                var pc = controller.Parent.Components.GetComponent<PhisycComponent>();
                //add bullet
                //controller.Parent.Childs.AddChild();

            };
            /*Control entity*/
            StateKeyboardLayout.Add(new BindibleKey("TEST_STATE_W", () => { return Keyboard.GetState().IsKeyDown(Keys.W); }, () => { entityController.Move(Vector3.Down); }));
            StateKeyboardLayout.Add(new BindibleKey("TEST_STATE_A", () => { return Keyboard.GetState().IsKeyDown(Keys.A); }, () => { entityController.Move(Vector3.Left); }));
            StateKeyboardLayout.Add(new BindibleKey("TEST_STATE_S", () => { return Keyboard.GetState().IsKeyDown(Keys.S); }, () => { entityController.Move(Vector3.Up); }));
            StateKeyboardLayout.Add(new BindibleKey("TEST_STATE_D", () => { return Keyboard.GetState().IsKeyDown(Keys.D); }, () => { entityController.Move(Vector3.Right); }));
            StateKeyboardLayout.Add(new BindibleKey("TEST_STATE_Q", () => { return Keyboard.GetState().IsKeyDown(Keys.Space); }, () => { entityController.Shot(); }));
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
