using EMCS.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.GameEntityes.Interfaces;

namespace Tanks1990MG_csharp.Application.GameEntityes.Solutions.Fabric.Builders
{
    interface IEntityBuilder
    {
        IGameEntity Build();
        IGameEntity Build(EventArgs arg);
    }
}
