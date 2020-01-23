//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using System;
//using Tanks1990MG_csharp.Application.GameEntityes.Interfaces;
//using Tanks1990MG_csharp.Application.Interfaces;
//using Tanks1990MG_csharp.Application.Logic.Phisyc;

//namespace Tanks1990MG_csharp.Application.GameEntityes.Solutions
//{
//    class GameEntity : IGameEntity
//    {
//        #region Data
//        /// <summary>
//        /// Хранит данные о физическом представлении сущности
//        /// </summary>
//        private IPhisycModel _IPhisycModel;
//        /// <summary>
//        /// Хранит данные о методе отображения сущности
//        /// </summary>
//        private IRendererModel _IRendererModel;
//        /// <summary>
//        /// Именно контроллер изменяет состояния обьектов, может стрелять и тд, хранит в себе указатель на управляемый обьект. Функции для работы с сущностью
//        /// </summary>
//        private IControllerModel _IControllerModel;
//        /// <summary>
//        /// Глобальный уникальный индентефикатор
//        /// </summary>
//        public uint GUID { get; set; }
//        #endregion

//        #region Events
//        /// <summary>
//        /// Когда изменяеться контроллер
//        /// </summary>
//        public event Action<object, IControllerModel> OnControllerChanged;
//        /// <summary>
//        /// Когда изменено физическое представление
//        /// </summary>
//        public event Action<object, IPhisycModel> OnPhisycModelChanged;
//        /// <summary>
//        /// Когда изменено графическое представление
//        /// </summary>
//        public event Action<object, IRendererModel> OnRendererModelChanged;
//        #endregion

//        #region Data Accesos
//        public IPhisycModel PhisycModel { get { return _IPhisycModel; } set { _IPhisycModel = value; OnPhisycModelChanged?.Invoke(this, _IPhisycModel); } }
//        public IRendererModel RendererModel { get { return _IRendererModel; } set { _IRendererModel = value; OnRendererModelChanged?.Invoke(this, _IRendererModel); } }
//        public IControllerModel ControllerModel { get { return _IControllerModel; } set { _IControllerModel = value; OnControllerChanged?.Invoke(this, _IControllerModel); } }
//        public string Name { get; set; } = "";

       
//        #endregion

//        /// <summary>
//        /// Реактивное изменение параметров обьекта. Возможный баг! При замене граф представления можеть не обновляться позиция
//        /// </summary>
//        public GameEntity()
//        {
//            //при изменении контроллера говорим ему что мы его сущность
//            OnControllerChanged += (object sender, IControllerModel Controller) => { Controller.Entity = this; };
//            //Устанавливаем физической модели привязку к этой сущности
//            OnPhisycModelChanged += (object s, IPhisycModel model) =>{ model.Parent = this; };
//            //Устанавливаем модели отображения к этой сущности
//            OnRendererModelChanged += (object s, IRendererModel model) => { model.Parent = this; };

//#if DEBUG
//            OnControllerChanged += (object sender, IControllerModel Controller) => { Console.WriteLine($"Entity : {GUID} controller changed : {Controller}"); };
//            OnPhisycModelChanged += (object s, IPhisycModel model) => { Console.WriteLine($"Entity : {GUID} phisyc model changed : {model}"); };
//            OnRendererModelChanged += (object s, IRendererModel model) => { Console.WriteLine($"Entity : {GUID} render model changed : {model}"); };
//#endif
//        }

//        #region Methods

//        /// <summary>
//        /// Обновить компоненты обьекта, безопасный метод без Exeptions
//        /// </summary>
//        /// <param name="time">Время кадра</param>
//        public void Update(GameTime time)
//        {
//            //update phisyc model of entity
//            PhisycModel?.Update(time);
//            //update animation or nothing
//            RendererModel?.Update(time);
//            //update specification of controller
//            ControllerModel?.Update(time);
//        }

//        /// <summary>
//        /// Draw entity. Call RendererModel.Draw(target, states);
//        /// </summary>
//        /// <param name="target">RenderTarget</param>
//        /// <param name="states">RenderStates</param>
//        public void Draw(GraphicsDevice device)
//        {
//            RendererModel?.Draw(device);
//        }
//        #endregion
//    }
//}
