using System;
using System.Collections.Generic;
using Tanks1990MG_csharp.Application.InputMG.Interfaces;
using Tanks1990MG_csharp.Application.InputMG.Solutions;
using Tanks1990MG_csharp.Application.Interfaces;
using Tanks1990MG_csharp.Application.States.Interfaces;

namespace Tanks1990MG_csharp.Application.States
{
    public interface IAppState : IUpdatebleTime, IDrawable
    {
        /// <summary>
        /// Нужго ли еще доставлять данные после получения из фабрики
        /// </summary>
        bool Initialized { get; set; }
        /// <summary>
        /// Подписываеться из App 
        /// </summary>
        event Action<StateBuilder.StateID> ChangeStateRequest;
        /// <summary>
        /// Сохранение горячего состояния
        /// </summary>
        void Save();
        /// <summary>
        /// Восстановление 
        /// </summary>
        void Load();
        ///////// <summary>
        ///////// поставщик графики
        ///////// </summary>
        //////GraphicController GraphicController { get; set; }

        ///////// <summary>
        ///////// Интерфейс состояния
        ///////// </summary>
        //////TGUI.Gui GUI { get; set; }
        
        /// <summary>
        /// Не выгружать из памяти
        /// </summary>
        bool DontUnloadFromMemory { get; set; }
        List<IBindebleKey> StateKeyboardLayout { get;set; }
    }
}
