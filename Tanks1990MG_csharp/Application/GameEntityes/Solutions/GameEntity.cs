using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.GameEntityes.Interfaces;
using Tanks1990MG_csharp.Application.Interfaces;
using Tanks1990MG_csharp.Application.Logic.Phisyc;

namespace Tanks1990MG_csharp.Application.GameEntityes.Solutions
{
    class GameEntity : IGameEntity
    {
        #region Data
        private IPhisycModel _IPhisycModel;
        private IRendererModel _IRendererModel;
        /// <summary>
        /// Именно контроллер изменяет состояния обьектов, может стрелять и тд, хранит в себе указатель на управляемый обьект
        /// </summary>
        private IControllerModel _IControllerModel;
        public uint GUID { get; set; }

        #endregion

        #region Events
        public event Action<object, IControllerModel> OnControllerChanged;
        public event Action<object, IPhisycModel> OnPhisycModelChanged;
        public event Action<object, IRendererModel> OnRendererModelChanged;
        #endregion

        #region Data Accesos
        public IPhisycModel PhisycModel { get { return _IPhisycModel; } set { _IPhisycModel = value; OnPhisycModelChanged?.Invoke(this, _IPhisycModel); } }
        public IRendererModel RendererModel { get { return _IRendererModel; } set { _IRendererModel = value; OnRendererModelChanged?.Invoke(this, _IRendererModel); } }
        public IControllerModel ControllerModel { get { return _IControllerModel; } set { _IControllerModel = value; OnControllerChanged?.Invoke(this, _IControllerModel); } }
        #endregion

        /// <summary>
        /// Реактивное изменение параметров обьекта. Возможный баг! При замене граф представления можеть не обновляться позиция
        /// </summary>
        public GameEntity()
        {
            //при изменении контроллера говорим ему что мы его сущность
            OnControllerChanged += (object sender, IControllerModel Controller) =>{ Controller.MyEntity = sender as IGameEntity; };
            //on any new phisyc model
            OnPhisycModelChanged += (object s, IPhisycModel model) =>
            {
                if (RendererModel != null)
                {
                    //auto update drawble position
                    //PhisycModel.PositionChanged += (object sender, Vector2 position) => { (RendererModel.Source as Transformable).Position = position; };
                    //auti update rotation position
                    //PhisycModel.RotationChanged += (object sender, Vector2f rotation) => { (RendererModel.Source as Transformable).Rotation = rotation; };
                }
            };

            //OnIRendererModelChanged += (object s, IRendererModel model) =>
            //{
            //    Console.WriteLine("New model set!");
            //};
        }

        #region Methods

        /// <summary>
        /// Обновить компоненты обьекта
        /// </summary>
        /// <param name="time">Время кадра</param>
        public void Update(GameTime time)
        {
            //update phisyc model of entity
            PhisycModel.Update(time);
            //update animation or nothing
            RendererModel.Update(time);
        }

        /// <summary>
        /// Draw entity. Call RendererModel.Draw(target, states);
        /// </summary>
        /// <param name="target">RenderTarget</param>
        /// <param name="states">RenderStates</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            RendererModel.Draw(spriteBatch);
        }
        #endregion
    }
}
