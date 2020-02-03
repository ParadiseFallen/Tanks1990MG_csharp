using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMCS.Interfaces
{
    public interface IEntityComponent<T> : INotifyPropertyChanged
    {
        #region Data
        //ссылка на родителя
        T Parent { get; }
        //был ли активирован правильно, или еще требует активации
        bool IsActivated { get; }
        bool IsPartOfBehavior { get; set; }
        #endregion

        #region Methods
        //Попытка активации
        void Activate(T parrent);
        //попытка деактивации
        void Deactivate(T parrent);
        //сброс данных, для переиспользования компонента
        void Reset();
        #endregion

        #region Events
        //срабатывает когда активировалось успешно
        event Action<IEntityComponent<T>> OnActivated;
        //срабатывает при успешной деактивации
        event Action<IEntityComponent<T>> OnDeactevated;
        //срабатывает при сбросе
        event Action<IEntityComponent<T>> OnReset;
        #endregion
    }
}
