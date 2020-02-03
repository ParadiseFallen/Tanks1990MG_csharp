using EMCS.Interfaces.Entity;
using EMCS.Realisations.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.Providers.Interfaces;

namespace Tanks1990MG_csharp.Application.GameEntityes.Solutions.Fabric.EntityProvider
{
    class CustomEntityProvider : IProvider<IEntity>
    {
        public Predicate<string> Filter { get; set; }
        public IEntity Get()
        {
            return new Entity();
        }
        public void Place(IEntity data)
        {
        }
    }
}
