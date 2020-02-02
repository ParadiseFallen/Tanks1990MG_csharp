using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Tanks1990MG_csharp.Application.Interfaces;

namespace Tanks1990MG_csharp.Application.States
{
    public class GraphicView 
    {
        public enum SortMode { BackToFront,FrontToBack};
        public SortMode DrawableSortMode { get; set; }

        public List<IDrawable> Drawables { get; } = new List<IDrawable>();

        public GraphicsDevice GDevice { get; set; }
        public SpriteBatch SpriteBatchRes { get; set; }
        public GraphicView()
        {
            SpriteBatchRes = new SpriteBatch(GDevice);
        }
        public void Draw()
        {
            Sort();
            Drawables.ForEach(i => i.Draw(this));
        }
        private void Sort()
        { 
            
        }
    }
}
