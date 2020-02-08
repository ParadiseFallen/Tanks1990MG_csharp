using EMCS.Realisations.Signatures;
using EMCS.Systems.SubSystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.ECS.Components;
using Tanks1990MG_csharp.Application.ECS.Dependencies;

namespace Tanks1990MG_csharp.Application
{
    class RenderSystem
    {
        public RenderTarget2D RenderTarget { get; set; }
        public DrawData DrawData { get; set; } = new DrawData() { };

        public List<IAutoDraw> Drawables { get; set; } = new List<IAutoDraw>();

        #region Instancing
        static private RenderSystem _Instance;
        static public RenderSystem Instance { get { if (_Instance == null) _Instance = new RenderSystem(); return _Instance; } }
        #endregion
        
        public RenderSystem()
        {
            
            /*Create render target*/
        }
        public void Init(GraphicsDeviceManager Manager,ContentManager Content)
        {
            DrawData.Graphics = Manager;
            DrawData.SpriteBatch = new SpriteBatch(Manager.GraphicsDevice);
            RenderTarget = new RenderTarget2D(
                    Manager.GraphicsDevice,
                    1920, 1080,
                    false,
                    Manager.GraphicsDevice.PresentationParameters.BackBufferFormat,
                    DepthFormat.Depth24);
            DrawData.Content = Content;
        }

        private void Sort()
        {
            Drawables.Sort((a,b)=> {
                if ((a as IAutoDraw).ZPoz > (a as IAutoDraw).ZPoz)
                    return 1;
                if ((a as IAutoDraw).ZPoz < (a as IAutoDraw).ZPoz)
                    return -1;
                return 0;
            });
        }

        private void DrawToBufferRenderTarget()
        { 
            DrawData.Graphics.GraphicsDevice.SetRenderTarget(RenderTarget);
            DrawData.Graphics.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            DrawData.Graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            DrawData.SpriteBatch.Begin();

            Drawables.ForEach((i) => { (i as IAutoDraw).Draw(DrawData); });

            DrawData.SpriteBatch.End();
            DrawData.Graphics.GraphicsDevice.SetRenderTarget(null);
        }
        /*Draw*/
        public void Draw(GameTime gameTime)
        {
            Sort();
            Drawables.ForEach(i => i.Update(gameTime));
            DrawToBufferRenderTarget();

            /*draw to screen*/
            DrawData.Graphics.GraphicsDevice.Clear(Color.Black);

            DrawData.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                        SamplerState.LinearClamp, DepthStencilState.Default,
                        RasterizerState.CullNone);

            DrawData.SpriteBatch.Draw(RenderTarget,destinationRectangle: new Rectangle(0,0,1920,1080));
            /*Не рисует*/

            //Drawables.ForEach((i) => { (i as IAutoDraw).Draw(DrawData); });



            DrawData.SpriteBatch.End();
        }

    }
}
