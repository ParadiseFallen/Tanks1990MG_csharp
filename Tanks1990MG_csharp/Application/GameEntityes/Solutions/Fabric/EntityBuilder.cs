using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.GameEntityes.Interfaces;
using Tanks1990MG_csharp.Application.GameEntityes.Solutions.Fabric.Builders;

namespace Tanks1990MG_csharp.Application.GameEntityes.Solutions
{
    class EntityBuilder
    {

        private uint counter;
        public EntityBuilder()
        {
            counter = 0;
        }
        public Dictionary<string, IEntityBuilder> Builders { get; private set; } = new Dictionary<string, IEntityBuilder>();

        private IEntityBuilder builder;
        public IGameEntity Build()
        {
            var t = builder.Build();
            t.GUID = counter++;
            return t;
        }
        public void ChangeBuilder(IEntityBuilder newBuilder)
        {
            builder = newBuilder;
        }

    }
}
