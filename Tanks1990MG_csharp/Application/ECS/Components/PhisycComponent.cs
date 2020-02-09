using EMCS.Interfaces.Entity;
using EMCS.Realisations.Components;
using EMCS.Realisations.Components.ComponetsBehavior;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990MG_csharp.Application.ECS.Components
{
    class PhisycComponent : EMCS.Realisations.Components.Component
    {
        public override event PropertyChangedEventHandler PropertyChanged;
        private Vector3 _Position = Vector3.Zero;
        public Vector3 Position { get { return _Position; } set { _Position = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Position")); } }
        private Vector3 _Rotation = Vector3.Zero;
        public Vector3 Rotation { get { return _Rotation; } set { _Rotation = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rotation")); } }
        private Vector3 _Acceleration = Vector3.Zero;
        public Vector3 Acceleration { get { return _Acceleration; } set { _Acceleration = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Acceleration")); } }
        public Vector3 PrevPos { get; set; }

        public override void WayToAtivate(IEntity parrent)
        {
            IsActivated = true;
            base.WayToAtivate(parrent);
        }
    }
}
