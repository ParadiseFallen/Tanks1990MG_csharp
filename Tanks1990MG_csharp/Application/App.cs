using ECS.Fabric;
using Microsoft.CSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.ECS.Fabric.Decorators;
using Tanks1990MG_csharp.Application.ECS.FabricDecorators;
using Tanks1990MG_csharp.Application.ECS.Systems;
using Tanks1990MG_csharp.Application.Game.GameEntityes.Fabric.Decorators;
using Tanks1990MG_csharp.Application.InputMG.Solutions;
using Tanks1990MG_csharp.Application.States;
using Tanks1990MG_csharp.Application.States.Interfaces;

namespace Tanks1990MG_csharp.Application
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class App : Microsoft.Xna.Framework.Game
    {
        #region Data
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        InputMG.Interfaces.IBindebleInputDevice keyboard;

        private IAppState currentState;
        #endregion

        #region Events
        public Action<GameTime> OnLogicUpdate;
        #endregion
        public App()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            //Myra.MyraEnvironment.Game = this;

            //register exit
            KeyInterpretator.Instance.RegisterAction("Game.Exit();", Exit);
            KeyInterpretator.Instance.Provider = new Providers.Solutions.SampleKeyFileProvider() { Link = "file.ly" };

            EntityBuilder.BuilderInstance.Content = Content;


            EntityBuilder.BuilderInstance.Decorators.Add("PhisycModelDecorator", new PhisycModelDecorator());
            EntityBuilder.BuilderInstance.Decorators.Add("TankTextureSprite", new RendererDecorator());
            EntityBuilder.BuilderInstance.Decorators.Add("ColisionDecorator", new ColisionDecorator());
            EntityBuilder.BuilderInstance.Decorators.Add("ControllerDecorator", new ControlDecorator());


            //PrefabList.Instance.Prefabs.Add("Tank",new Prefab() { Name= "Tank", Chain = new DecorationChain() { EntityProviderName = "CustomEntity", DecoratorsChain = { "PhisycModelDecorator", "TankTextureSprite" } } });

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// инициализация не графики
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "Tanks1990";
            IsMouseVisible = true;

            //GeonBit.UI.UserInterface.Initialize(Content);
            //init keyboard
            keyboard = new BindableInputDevice();

            Components.Add(keyboard);
            //update on logic update
            OnLogicUpdate += (GameTime t) => { };
            //link key interpretator
            keyboard.KeyAdded += KeyInterpretator.Instance.RegisterSample;
            //load layout
            KeyInterpretator.Instance.LoadLayout(keyboard as BindableInputDevice);

            //MonoGame.UI.Forms.Form f = new MonoGame.UI.Forms.Form();

            // UI = new GeonBit.UI.UserInterface();
            // UI.AddEntity(new GeonBit.UI.Entities.Button());
            // var Panel = new Myra.Graphics2D.UI.Panel() { Id = "DeveloperMenu", Layout2d = new Myra.Graphics2D.UI.Properties.Layout2D("this.h = W.h;this.w = W.w") };
            //Panel.Widgets.Add(new Myra.Graphics2D.UI.Window() { Title = "Myra.Graphics2D.UI.Panel.DeveloperMenu" });
            //keyboard.AddKey(new BindibleKey("DeveloperMenu_Tilda_LAlt", () => Keyboard.GetState().GetPressedKeys().ToList().Contains(Keys.LeftAlt) && Keyboard.GetState().GetPressedKeys().ToList().Contains(Keys.OemTilde), () => { Myra.Graphics2D.UI.Desktop.GetWidgetByID("DeveloperMenu").Visible = !Myra.Graphics2D.UI.Desktop.GetWidgetByID("DeveloperMenu").Visible; }) { RepeatDelayMS = 100 });
            //Myra.Graphics2D.UI.Desktop.Widgets.Add(Panel);

            Task.Run(() => ChangeState(StateBuilder.StateID.TEST)).Wait();

            base.Initialize();

            RenderSystem.Instance.Init(graphics, Content);
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            currentState?.Update(gameTime);

            //UI.Update(gameTime);

            // TODO: Add your update logic here
            OnLogicUpdate?.Invoke(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            RenderSystem.Instance.Draw(gameTime);
            //Myra.Graphics2D.UI.Desktop.Render();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        /// <summary>
        /// Изменить активное состояние
        /// </summary>
        /// <param name="newStateID">ID нового состояния</param>
        private async void ChangeState(StateBuilder.StateID newStateID)
        {
            //old state
            if (currentState != null)
                LinkState(currentState, false);
            /*
             LOADING SCREEN
             Нужно легкое состояние, которое будет быстро загружаться, Фон картинка, какой либо текст подсказка(игровая), иконка загрузки
             
             */

            //new state
            currentState = await StateBuilder.GetStateAsync(newStateID);
            LinkState(currentState, true);
            //Myra.Graphics2D.UI.Desktop.Widgets.Add();


            if (currentState.Initialized)
                return;
            currentState.ChangeStateRequest += ChangeState;
            currentState.Initialized = true;
        }

        private void LinkState(IAppState appState, bool Link)
        {
            if (Link)
            {
                appState?.Load();

                if (appState.StateKeyboardLayout != null)
                    keyboard?.AddRange(appState.StateKeyboardLayout);
                //if (appState.GUI != null)
                    //Myra.Graphics2D.UI.Desktop.Widgets.Add(appState.GUI);
            }
            else
            {
                appState?.Save();

                if (appState.StateKeyboardLayout != null)
                    keyboard?.RemoveRange(appState.StateKeyboardLayout);
                //if (appState.GUI != null)
                    //Myra.Graphics2D.UI.Desktop.Widgets.Remove(appState.GUI ?? null);
            }
        }
    }
}
