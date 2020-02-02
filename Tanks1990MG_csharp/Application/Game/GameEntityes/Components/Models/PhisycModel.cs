using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMCS.Interfaces;
using EMCS.Interfaces.Components.Component;
using EMCS.Interfaces.Components.Model;
using EMCS.Interfaces.Entity;
using EMCS.Realisations.Components.Model;
using EMCS.Realisations.Signatures;
using Microsoft.Xna.Framework;

namespace Tanks1990MG_csharp.Application.GameEntityes.Components
{
    class PhisycModel : StandartModel
    {
        public PositionComponent PosComponent { get; set; }
        public override void WayToAtivate(IGameEntity parrent)
        {
            PosComponent = GetComponent<PositionComponent>();

            base.WayToAtivate(parrent);
        }
    }
}
