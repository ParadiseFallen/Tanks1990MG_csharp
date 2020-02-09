using EMCS.Interfaces.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMCS.Realisations.System
{
    public class SystemStorage : ISystemStorage
    {
        public List<ISystem> Systems { get; } = new List<ISystem>();
        public event Action<object, ISystem> OnAddSystem;
        public event Action<object, ISystem> OnRemoveSystem;

        /// <summary>
        /// Add new ECSSystem
        /// </summary>
        /// <param name="system">New system to add</param>
        /// <returns>Sucsess or not</returns>
        public bool AddSystem(ISystem system)
        {
            if (Systems.Contains(system))
                return false;
            Systems.Add(system);
            OnAddSystem?.Invoke(this, system);
            return true;
        }
        public T GetSystem<T>() where T : class, ISystem
        {
            foreach (var item in Systems)
            {
                if (item is T)
                    return item as T;
            }
            return default;
        }
        public ISystem GetSystem(Type SystemType)
        {
            foreach (var item in Systems)
            {
                if (item.GetType() == SystemType)
                    return item;
            }
            return default;
        }
        public ISystem GetSystem(ISystem System)
        {
            foreach (var item in Systems)
            {
                if (item == System)
                    return item;
            }
            return default;
        }
        public bool RemoveSystem(ISystem system)
        {
            return Systems.Remove(system);
        }
    }
}
