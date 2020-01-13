using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.Interfaces;

namespace Tanks1990MG_csharp.Application.Logic.Phisyc
{
    class ObjectPhisycModel2d : IPhisycModel
    {
        #region Data
        private int _AccelerationAttenuationRate0_100;
        private IColision2D _MyColison;
        private Vector2 _Acceleration;
        private Vector2 _Position;
        private Vector2 _Rotation;

        #endregion

        #region Data Accessores
        public int AccelerationAttenuationRate0_100 { get { return _AccelerationAttenuationRate0_100; } set { _AccelerationAttenuationRate0_100 = value; IPhisycModelUpdated?.Invoke(this); } }
        public IColision2D MyColison { get { return _MyColison; } set { _MyColison = value; IPhisycModelUpdated?.Invoke(this); } }
        public Vector2 Acceleration { get { return _Acceleration; } set { _Acceleration = value; AccelerationChanged?.Invoke(this, _Acceleration); IPhisycModelUpdated?.Invoke(this); } }
        public Vector2 Position { get { return _Position; } set { _Position = value; PositionChanged?.Invoke(this, _Position); IPhisycModelUpdated?.Invoke(this); } }
        public Vector2 Rotation { get { return _Rotation; } set { _Rotation = value; RotationChanged?.Invoke(this, _Rotation); IPhisycModelUpdated?.Invoke(this); } }

        #endregion

        #region Events
        public event Action<IPhisycModel> IPhisycModelUpdated;
        public event Action<object, Vector2> AccelerationChanged;
        public event Action<object, Vector2> PositionChanged;
        public event Action<object, Vector2> RotationChanged;
        #endregion

        /// <summary>
        /// Acceleration += acceleration;
        /// </summary>
        /// <param name="acceleration"></param>
        public void Accelerate(Vector2 acceleration)
        {
            Acceleration += acceleration;
        }

        /// <summary>
        /// Update acceleration and pos by time
        /// </summary>
        /// <param name="time">Frame time</param>
        public void Update(GameTime time)
        {
            //speed up object
            Position += Acceleration * time.ElapsedGameTime.Seconds;
            //brake object
            Acceleration = Acceleration - (Acceleration * (AccelerationAttenuationRate0_100 / 100));
            IPhisycModelUpdated?.Invoke(this);
        }
    }
}
