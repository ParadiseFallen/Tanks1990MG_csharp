﻿using System;
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
        public float RotationDeg { get; set; } = 0;
        public int ZPoz { get; set; } = 0;
        public bool Enabled { get; set; }
        public int UpdateOrder { get; set; }
        private Texture2D _Texture;
        public  Vector2 Origin { get; set; }
        public Texture2D Texture { get { return _Texture; } set { _Texture = value; Origin = new Vector2(_Texture.Width/2,_Texture.Height/2); } }

        #region Events
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
        #endregion

        public void Draw(DrawData drawData)
        {
            drawData.SpriteBatch.Draw(Texture, position: new Vector2(Position.X, Position.Y),scale : new Vector2(Scale.X,Scale.Y),rotation : RotationDeg,origin: Origin);
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
