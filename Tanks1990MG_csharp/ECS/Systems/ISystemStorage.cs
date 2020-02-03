using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMCS.Interfaces.System
{
    public interface ISystemStorage
    {
        List<ISystem> Systems { get; }
        //получить уже типизированный компонент по его сигнатуре
        T GetSystem<T>() where T : class, ISystem;
        //получить компонент по сигнатуре
        ISystem GetSystem(Type SystemType);
        //получить компонент по экземпляру
        ISystem GetSystem(ISystem System);
        //добавить компонент
        bool AddSystem(ISystem component);
        //убрать компонент
        bool RemoveSystem(ISystem component);

        event Action<object, ISystem> OnAddSystem;
        event Action<object, ISystem> OnRemoveSystem;
    }
}
