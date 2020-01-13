using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.InputMG.Interfaces;

namespace Tanks1990MG_csharp.Application.InputMG.Solutions
{
    /// <summary>
    ///Bindeble keyboard 
    /// </summary>
    class BindableInputDevice : IBindebleInputDevice
    {
        #region Data
        /// <summary>
        /// List of bindible keys
        /// </summary>
        private List<IBindebleKey> Keys { get; set; }
        /// <summary>
        /// UNSAFE! get list of Keys
        /// </summary>
        /// <returns>List<BindibleKey></returns>
        public List<IBindebleKey> UnsafeGetKeys() { return Keys; }
        #endregion
        #region Events
        /// <summary>
        /// Action called when key added, you can Connect KeyInterpretator.RegisterSample(), 
        /// Action will called only when you send metadata
        /// </summary>
        public event Action<KeyMetadata> KeyAdded;
        #endregion
        #region Work with keys
        /// <summary>
        /// Add new key to list, if you send metadata KeyAdded will be invoked
        /// </summary>
        /// <param name="key">Bindible key</param>
        /// <param name="metadata">Info of key in KeyMetadata</param>
        public void AddKey(IBindebleKey key, KeyMetadata metadata = null)
        {
            if (Keys.Find(i => i.Description == key.Description) != null) throw new Exception("Key already exist!");
            Keys.Add(key);
            if (metadata != null)
                KeyAdded.Invoke(metadata);
        }
        /// <summary>
        /// Bind action to key by key description,
        /// key must be already created
        /// </summary>
        /// <param name="KeyDescription">Description of key</param>
        /// <param name="action">Bindeble action</param>
        public void BindToKey(string KeyDescription, Action action)
        {
            var key = GetKey(KeyDescription);
            if (key == null)
            {
                throw new Exception($"Cant find key with description {KeyDescription}");
            }
            key.Trigered += action;
        }
        /// <summary>
        /// Change triger on key by descr
        /// </summary>
        /// <param name="KeyDescription">Key description</param>
        /// <param name="Triger">New key triger</param>
        public void ChangeTriger(string KeyDescription, Func<bool> Triger)
        {
            GetKey(KeyDescription).Triger = Triger;
        }
        /// <summary>
        /// Create key with description
        /// </summary>
        /// <param name="KeyDescription">Key description</param>
        public void CreateKeyWithDescription(string KeyDescription)
        {
            if (GetKey(KeyDescription) is null)
            {
                Keys.Add(new BindibleKey(KeyDescription, null, null));
            }
        }
        /// <summary>
        /// find key by description
        /// </summary>
        /// <param name="KeyDescription">Key description</param>
        /// <returns></returns>
        public IBindebleKey GetKey(string KeyDescription)
        {
            return Keys.Find(i => i.Description == KeyDescription);
        }
        /// <summary>
        /// Remove all key with descr
        /// </summary>
        /// <param name="KeyDescription">Description of key</param>
        /// <returns></returns>
        public int DeleteKey(string KeyDescription)
        {
            return Keys.RemoveAll(i => i.Description == KeyDescription);
        }
        /// <summary>
        /// Lock all keys with descr
        /// </summary>
        /// <param name="description">list of descr keys</param>
        public void LockKeys(Predicate<String> Filter)
        {
            Keys.ForEach(
                i => {
                    if (Filter(i.Description)) i.Locked = true;
                }
                );
        }
        public void UnlockKeys(Predicate<String> Filter)
        {
            Keys.ForEach(
                    i => {
                        if (Filter(i.Description)) i.Locked = false;
                    }
                    );
        }
        #endregion
        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="Keys">Layout|List<BindibleKey> keys|DEFAULT = null</param>
        public BindableInputDevice(List<IBindebleKey> Keys = null)
        {
            this.Keys = Keys;
            //check null
            if (Keys is null) this.Keys = new List<IBindebleKey>();
        }
        /// <summary>
        /// Send all keys KeyEventArgs
        /// </summary>
        public void Update()
        {
            Keys.ForEach(i => i.Update());
        }

        public void AddRange(List<IBindebleKey> keys)
        {
            Keys.AddRange(keys);
        }

        public void RemoveRange(List<IBindebleKey> keys)
        {
            Keys = Keys.Except(keys).ToList();
        }
    }
}
