using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.InputMG.Solutions;

namespace Tanks1990MG_csharp.Application.InputMG.Interfaces
{
    /// <summary>
    /// Interface for custom bindible input device
    /// </summary>
    interface IBindebleInputDevice
    {
        /// <summary>
        /// Action called when key added
        /// </summary>
        event Action<KeyMetadata> KeyAdded;
        /// <summary>
        /// Update method
        /// </summary>
        void Update();
        /// <summary>
        /// Добавить новую IBindebleKey<T> 
        /// </summary>
        /// <param name="key">IBindebleKey<T> object</param>
        /// <param name="metadata">Description of object optionaly</param>
        void AddKey(IBindebleKey key, KeyMetadata metadata = null);
        /// <summary>
        /// Bind new action to key
        /// </summary>
        /// <param name="KeyDescription">Description of key</param>
        /// <param name="action">Action</param>
        void BindToKey(string KeyDescription, Action action);
        /// <summary>
        /// Change key triger
        /// </summary>
        /// <param name="KeyDescription">Description of key</param>
        /// <param name="Triger">New triger</param>
        void ChangeTriger(string KeyDescription, Func<bool> Triger);
        /// <summary>
        /// Create empty key with description
        /// </summary>
        /// <param name="KeyDescription">description</param>
        void CreateKeyWithDescription(string KeyDescription);
        /// <summary>
        /// Get key by description
        /// </summary>
        /// <param name="KeyDescription">Description</param>
        /// <returns></returns>
        IBindebleKey GetKey(string KeyDescription);
        /// <summary>
        /// Delete keys with description
        /// </summary>
        /// <param name="KeyDescription">Description of key</param>
        /// <returns>Count if deleted keys</returns>
        int DeleteKey(string KeyDescription);
        /// <summary>
        /// Lock keys(dont handle events)
        /// </summary>
        /// <param name="description">List of descriptions</param>
        void LockKeys(Predicate<String> Filter);
        /// <summary>
        /// Unlock by description (Handle events again)
        /// </summary>
        /// <param name="description">List of description</param>
        void UnlockKeys(Predicate<String> Filter);
        /// <summary>
        /// Add range of keys
        /// </summary>
        /// <param name="Keys">List of keys</param>
        void AddRange(List<IBindebleKey> Keys);
        /// <summary>
        /// Remove keys
        /// </summary>
        /// <param name="Keys">List of keys</param>
        void RemoveRange(List<IBindebleKey> Keys);

    }
}
