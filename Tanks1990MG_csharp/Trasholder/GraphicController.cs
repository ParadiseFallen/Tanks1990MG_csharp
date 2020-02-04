//using ECSSystem;
//using ECSSystem.Collections.Generic;
//using ECSSystem.Collections.ObjectModel;
//using ECSSystem.Collections.Specialized;
//using ECSSystem.Linq;
//using ECSSystem.Text;
//using ECSSystem.Threading.Tasks;
//using Microsoft.Xna.Framework.Graphics;
//using Tanks1990MG_csharp.Application.Interfaces;

//namespace Tanks1990MG_csharp.Application.GameEntityes
//{
//    //instancing!
//    class GraphicController : IDrawable
//    {
//        public ObservableCollection<IDrawable> Drawables { get; set; } = new ObservableCollection<IDrawable>();
//        public List<IDrawable> DrawablesAsList { get { return Drawables.ToList(); } }

//        public void UpdateCollection(object sender, NotifyCollectionChangedEventArgs e) {
//            if (e.Action == NotifyCollectionChangedAction.Add)
//            {

//            }
//            if (e.Action== NotifyCollectionChangedAction.Remove)
//            {

//            }
//        }


//        public void Draw(GraphicsDevice graphicsDevice)
//        {
//            //throw new NotImplementedException();
//        }
//    }
//}
