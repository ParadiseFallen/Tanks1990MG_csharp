using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tanks1990MG_csharp.Application.ECS.Dependencies
{
    class Sprite2D : IAutoDraw
    {
        public Vector3 Position { get; set; } = Vector3.Zero;
        public Vector3 Rotation { get ; set ; } = Vector3.Zero;
        public Vector3 Scale { get ; set ; } = Vector3.Zero;
        public int ZPoz { get; set; } = 0;
        public bool Enabled { get; set; }
        public int UpdateOrder { get; set; }
        public Texture2D Texture { get; set; }

        #region Events
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
        #endregion

        public void Draw(DrawData drawData)
        {
            drawData.SpriteBatch.Draw(Texture, position: new Vector2(Position.X, Position.Y),scale : new Vector2(Scale.X,Scale.Y));
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
