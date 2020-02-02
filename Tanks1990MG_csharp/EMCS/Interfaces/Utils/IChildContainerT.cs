using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMCS.Interfaces
{
    public interface IChildContainer<T>
    {
        //у сущности могут быть наследники
        List<T> ChildEntities { get; }
        void AddChild(T entity);
        bool RemoveChild(T entity);
        T GetChild(Func<T, bool> Filter);
        T RecursiveGetChild(Func<T, bool> Filter, int depth = 10);

        event Action<object, T> OnChildAdded;
        event Action<object, T> OnChildRemoved;
    }
}
