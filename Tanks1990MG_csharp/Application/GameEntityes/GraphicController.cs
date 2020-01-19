using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Tanks1990MG_csharp.Application.Interfaces;

namespace Tanks1990MG_csharp.Application.GameEntityes
{
    class GraphicController : IDrawable
    {
        public ObservableCollection<IDrawable> Drawables { get; set; } = new ObservableCollection<IDrawable>();
        public List<IDrawable> DrawablesAsList { get { return Drawables.ToList(); } }

        public void UpdateCollection(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {

            }
            if (e.Action== NotifyCollectionChangedAction.Remove)
            {

            }
        }


        public void Draw(GraphicsDevice graphicsDevice)
        {
            throw new NotImplementedException();
        }
    }
}
