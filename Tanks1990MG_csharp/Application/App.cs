using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using Myra.Graphics2D.UI;
using Tanks1990MG_csharp.Application.InputMG.Solutions;
using Tanks1990MG_csharp.Application.States;
using Tanks1990MG_csharp.Application.States.Interfaces;

namespace Tanks1990MG_csharp.Application
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class App : Game
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
            Myra.MyraEnvironment.Game = this;
            

            //register exit
            KeyInterpretator.Instance.RegisterAction("Game.Exit();", Exit);
            KeyInterpretator.Instance.Provider = new Providers.Solutions.SampleKeyFileProvider() { Link = "file.ly" };
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

            //init keyboard
            keyboard = new BindableInputDevice();
            //update on logic update
            OnLogicUpdate += (GameTime t) => {  };
            //link key interpretator
            keyboard.KeyAdded += KeyInterpretator.Instance.RegisterSample;
            //load layout
            KeyInterpretator.Instance.LoadLayout(keyboard as BindableInputDevice);


            Myra.MyraEnvironment.Game = this;
            Desktop.HasExternalTextInput = true;

            // Provide that text input
            Window.TextInput += (s, a) =>{Desktop.OnChar(a.Character);};

            ChangeState(StateBuilder.StateID.TEST);

            base.Initialize();
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
            currentState.Update(gameTime);

            keyboard.Update();
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
            GraphicsDevice.Clear(Color.Bisque);
            spriteBatch.Begin();

            currentState?.Draw(spriteBatch);
            Desktop.Render();
            // TODO: Add your drawing code here

            spriteBatch.End();
            base.Draw(gameTime);
        }


        /// <summary>
        /// Изменить активное состояние
        /// </summary>
        /// <param name="newStateID">ID нового состояния</param>
        private void ChangeState(StateBuilder.StateID newStateID)
        {
            //old state
            if (currentState != null)
            {
                if (currentState.DontUnloadFromMemory) currentState?.Save();
                keyboard?.RemoveRange(currentState.StateKeyboardLayout);
            }
            //new state
            currentState = StateBuilder.GetState(newStateID);
            keyboard?.AddRange(currentState.StateKeyboardLayout);
            if (currentState.Initialized)
            {
                return;
            }
            currentState.Initialized = true;
            //Load gui
            //windowContainer.Resized += (object sender, SizeEventArgs arg) => { currentState.GUI.View = Camera; };
            //windowContainer.Subscribe((RenderWindow window) => { currentState.GUI.Target = window; });
            //currentState.GUI.Target = windowContainer.Window;
            currentState.ChangeStateRequest += ChangeState;
        }
    }
}
