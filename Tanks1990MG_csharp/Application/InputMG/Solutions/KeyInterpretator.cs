using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.InputMG.Interfaces;
using Tanks1990MG_csharp.Application.Providers.Interfaces;

namespace Tanks1990MG_csharp.Application.InputMG.Solutions
{
    class KeyInterpretator
    {
        /// <summary>
        /// Провайдер который предоставляет данные <List<KeyMetadata>>
        /// </summary>
        public IProvider<List<KeyMetadata>> Provider { get; set; }
        /// <summary>
        /// Режимы добавления сэмплов и загрузки,
        /// Как добавить кнопки в клавиатуру Замена/Добавление, загрузка примеров из файла Замена/Добавление
        /// </summary>
        public enum AddMode { ADD, REPLACE }
        public enum LoadMode { DEFAULT, PROVIDER }

        #region Instancing
        /// <summary>
        /// Общая точка доступа, что бы можно было регистрировать функции из любой части программы
        /// </summary>
        static private KeyInterpretator instance;
        /// <summary>
        ///  Получить точку доступа
        /// </summary>
        /// <param name="provider">Провайдер с которым будет работать, по умолчанию null</param>
        /// <returns>Точка доступа KeyInterpretator</returns>
        /// 
        static public KeyInterpretator Instance
        {
            get
            {
                if (instance is null)
                {
                    instance = new KeyInterpretator();
                }
                return instance;
            }
        }
        private KeyInterpretator()
        {
            //init all fields
            KeyActivationFunctionsDictionary = new Dictionary<string, Func<bool>>();
            KeyActionsDictionary = new Dictionary<string, Action>();
            Samples = new List<KeyMetadata>();
            RegisterDefaultTrigers();
        }
        #endregion
        #region Data
        /// <summary>
        /// Словарь описание - Функция активации (Тригер)
        /// </summary>
        public Dictionary<string, Func<bool>> KeyActivationFunctionsDictionary { get; set; }
        /// <summary>
        /// Словарь описание - Действие
        /// </summary>
        public Dictionary<string, Action> KeyActionsDictionary { get; set; }
        /// <summary>
        /// Список примеров клавиш 
        /// </summary>
        private List<KeyMetadata> Samples;
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="provider">Провайдер, с которым будет работать интерпретатор</param>
        //public KeyInterpretator(IProvider<List<KeyMetadata>> provider)
        //{
        //    //set provider
        //    Provider = provider;
        //    //register default trigers
        //}

        #region Registering
        /*
         RULES:
         One action - one description
         one triger - one description
         one key - one description many Actions, one triger
        */
        #region Registering
        /// <summary>
        /// Зарегестрировать действие
        /// </summary>
        /// <param name="key">Ключ(Имя) действия RULE: CLASS_NAME.METHOD()</param>
        /// <param name="action">Само действие, которое будет привязано к ключу</param>
        public void RegisterAction(string key, Action action)
        {
            if (KeyActionsDictionary.Keys.ToList().Contains(key)) throw new Exception($"Key {key} already registered like {action}");
            if (KeyActionsDictionary.Values.ToList().Contains(action)) throw new Exception($"Action {action} already registered like {key}");
            KeyActionsDictionary.Add(key, action);
        }
        /// <summary>
        /// Зарегестрировать тригер(функцию активации)
        /// </summary>
        /// <param name="key">Ключ(Имя) тригера пример: Pressed_Escape</param>
        /// <param name="action">Тригер, который будет привязан к ключу</param>
        public void RegisterTriger(string key, Func<bool> triger)
        {
            if (KeyActivationFunctionsDictionary.Keys.ToList().Contains(key)) throw new Exception($"Key {key} already registered like {triger}");
            if (KeyActivationFunctionsDictionary.Values.ToList().Contains(triger)) throw new Exception($"Triger {triger} already registered like {key}");
            KeyActivationFunctionsDictionary.Add(key, triger);
        }
        /// <summary>
        /// Регистрация примера клавиши
        /// </summary>
        /// <param name="value">Пример клавиши</param>
        public void RegisterSample(KeyMetadata value)
        {
            if (Samples.Find(i => i.Description == value.Description) != null) throw new Exception($"Key {value.Description} already registered");
            if (!CheckForExist(KeyActionsDictionary.Keys.ToList(), value.ActionF)) throw new Exception($"Some actions dont registered");
            Samples.Add(value);
        }
        #endregion
        #region Unregistering
        /// <summary>
        /// Удалить пример по описанию
        /// </summary>
        /// <param name="Description">Описание примера</param>
        /// <returns>Кол-во удаленных примеров, если больше 1 значить в программе баг</returns>
        public int UnregisterSample(string Description)
        {
            return Samples.RemoveAll(i => i.Description == Description);
        }
        /// <summary>
        /// Удалить действие по описанию
        /// </summary>
        /// <param name="Key">Описание действия</param>
        /// <returns>Удачно или нет</returns>
        public bool UnregisterAction(string Key)
        {
            return KeyActionsDictionary.Remove(Key);
        }
        /// <summary>
        /// Удалить тригер по описанию
        /// </summary>
        /// <param name="Key">Описание тригера</param>
        /// <returns>Удачно или нет</returns>
        public bool UnregisterTriger(string Key)
        {
            return KeyActivationFunctionsDictionary.Remove(Key);
        }
        #endregion
        #region GetData
        /// <summary>
        /// Получить пример по описанию
        /// </summary>
        /// <param name="Description">Описание примера клавиши</param>
        /// <returns>Пример клавиши</returns>
        public KeyMetadata GetSample(string Description)
        {
            return Samples.Find(i => i.Description == Description);
        }
        /// <summary>
        /// Получить действие по описанию
        /// </summary>
        /// <param name="Key">Описание действия</param>
        /// <returns>Действие</returns>
        public Action GetAction(string Key)
        {
            return KeyActionsDictionary[Key];
        }
        /// <summary>
        /// Получить тригер по описанию
        /// </summary>
        /// <param name="Key">Описание тригера</param>
        /// <returns>Тригер</returns>
        public Func<bool> GetTriger(string Key)
        {
            return KeyActivationFunctionsDictionary[Key];
        }
        #endregion
        #endregion
        #region Tools
        /// <summary>
        /// Проверить на наличие в словаре
        /// </summary>
        /// <typeparam name="T">Тип, который будет сравниваться</typeparam>
        /// <param name="dictionary">Словарь</param>
        /// <param name="values">Значение, которое будет искать</param>
        /// <returns>Удачно/неудачно</returns>
        private bool CheckForExist<T>(List<T> dictionary, List<T> values)
        {
            foreach (var item in values)
            {
                if (!dictionary.Contains(item))
                    return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// Для сборки клавиш из примеров
        /// </summary>
        /// <returns>Список собранных клавиш</returns>
        private List<IBindebleKey> BuildBindibleKeysFromSamples()
        {
            List<IBindebleKey> keys = new List<IBindebleKey>();
            foreach (var item in Samples)
            {
                var t = new 
                    BindibleKey(item.Description, KeyActivationFunctionsDictionary[item.Triger], null) 
                    {RepeatDelayEnabled = item.RepeatDelayEnabled, RepeatDelayMS = item.RepeatDelayMS };
                //add all actions
                item.ActionF.ForEach(i => { t.Trigered += KeyActionsDictionary[i]; });
                keys.Add(t);
            }
            return keys;
        }

        #region Default values
        /// <summary>
        /// Добавить примеры по умолчанию
        /// </summary>
        private void RegisterDefaultSamples()
        {
            RegisterSample(new KeyMetadata(new List<string>() { "Game.Exit();" }) { Description = "EscapeProgramm", Triger = "Pressed_Escape" });
        }
        /// <summary>
        /// Добавить тригеры по умолчанию
        /// </summary>
        private void RegisterDefaultTrigers()
        {
            RegisterTriger("Pressed_Escape", (() => {return Keyboard.GetState().IsKeyDown(Keys.Escape); }));
        }
        #endregion
        #region Save-load
        /// <summary>
        /// Получает у провайдера данные о примерах
        /// </summary>
        /// <param name="loadMode">Режим загрузки По умолчанию/Провайдер</param>
        /// <param name="mode">Режим добавления Добавить/заменить</param>
        /// <returns>Удачно/нет, если нет то загрузит примеры по умолчанию</returns>
        public bool LoadSamples(LoadMode loadMode = LoadMode.DEFAULT, AddMode mode = AddMode.ADD)
        {
            if (loadMode == LoadMode.DEFAULT) return false;
            switch (mode)
            {
                case AddMode.ADD:
                    Samples.AddRange(Provider.Get());
                    break;
                case AddMode.REPLACE:
                    Samples = Provider.Get();
                    break;
                default:
                    throw new Exception("Wrong mode! You can use ADD|REPLACE");
            }
            if (Samples.Count <= 0)
            {
                RegisterDefaultSamples();
                return false;
            }
            return true;
        }
        /// <summary>
        /// Сохранение
        /// </summary>
        /// <param name="loadMode">Провайдер/По умолчанию</param>
        public void SaveSamples(LoadMode loadMode = LoadMode.DEFAULT)
        {
            if (loadMode == LoadMode.DEFAULT) return;
            Provider.Place(Samples);
        }
        #endregion

        /// <summary>
        /// Построить клавиши по примерам и загрузить в устройство
        /// </summary>
        /// <param name="device">Устройство для загрузки</param>
        /// <param name="mode">Режим добавления в устройство, по умолчанию добавление</param>
        public void LoadLayout(BindableInputDevice device, AddMode mode = AddMode.ADD)
        {
            var keys = device.UnsafeGetKeys();
            //load samples from file
            if (!LoadSamples() && Samples.Count <= 0) RegisterDefaultSamples();
            switch (mode)
            {
                case AddMode.ADD:
                    keys.AddRange(BuildBindibleKeysFromSamples());
                    break;
                case AddMode.REPLACE:
                    keys = BuildBindibleKeysFromSamples();
                    break;
                default:
                    throw new Exception("Wrong mode! You can use ADD|REPLACE");
            }
        }
    }
}
