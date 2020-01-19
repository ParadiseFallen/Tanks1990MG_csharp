using System;
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
    class EntityController : IUpdateable
    {
        public ObservableCollection<IGameEntity> Entities { get; private set; } = new ObservableCollection<IGameEntity>();
        public List<IGameEntity> EntitiesAsList { get {return Entities.ToList(); } }

        public bool Enabled { get; set; }

        public int UpdateOrder { get; set; }

        public EntityController()
        {
        }

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;

        public void Update(GameTime time)
        {

            EntitiesAsList.ForEach(i => i.Update(time));
        }
    }
}
