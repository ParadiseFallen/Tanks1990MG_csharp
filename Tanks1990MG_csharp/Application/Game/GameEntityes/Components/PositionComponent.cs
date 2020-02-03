using EMCS.Interfaces;
using EMCS.Interfaces.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990MG_csharp.Application.GameEntityes.Components
{

    class PositionComponent : EMCS.Realisations.Components.Component
    {
        public override event PropertyChangedEventHandler PropertyChanged;

        private Vector3 _Position;
        private Vector3 _Rotation;
        public Vector3 Position { get { return _Position; } set { _Position = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Position_Vector3")); } }
        public Vector3 Rotation { get { return _Rotation; } set { _Rotation = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rotation_Vector3")); } }

        public override void WayToAtivate(IEntity parrent)
        {
            IsActivated = true;
            base.WayToAtivate(parrent);
        }

        public override void WayToDeativate(IEntity parrent)
        {
            IsActivated = false;
            base.WayToDeativate(parrent);
        }
    }

}
