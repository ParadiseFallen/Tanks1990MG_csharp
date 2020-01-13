using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tanks1990MG_csharp.Application.GameEntityes.Interfaces;
using Tanks1990MG_csharp.Application.Interfaces;

namespace Tanks1990MG_csharp.Application.GameEntityes
{
    /// <summary>
    /// Можно делать масив чанков а каждый чанк хранит контроллер
    /// </summary>
    class EntityController : Application.Interfaces.IDrawable, IUpdatebleTime
    {
        public ObservableCollection<IGameEntity> Entities { get; private set; } = new ObservableCollection<IGameEntity>();
        
        public EntityController()
        {
            Entities.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) => { };

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Entities.ToList().ForEach(i=>i.Draw(spriteBatch));
        }

        public void Update(GameTime time)
        {
            Entities.ToList().ForEach(i => i.Update(time));
        }
    }
}
