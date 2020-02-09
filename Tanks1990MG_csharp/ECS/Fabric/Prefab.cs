using ECS.Fabric;
using EMCS.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Fabric
{
    [Serializable]
    class Prefab
    {
        //public Dictionary<string, dynamic> Settings { get; } = new Dictionary<string, dynamic>();
        public string Name { get; set; } = "";
        public DecorationChain Chain { get; set; }
        public IEntity Construct()
        {
            var w= EntityBuilder.BuilderInstance.GetWrap().StartBuild(Chain.EntityProviderName);
            foreach (var item in Chain.DecoratorsChain)
            {
                w.Decorate(item);
            }
            return w.Resault;
        }
    }


    class PrefabList
    { 

        static private PrefabList _Instance;
        static public PrefabList Instance { get { if (_Instance is null) _Instance = new PrefabList(); return _Instance; } }

        public Dictionary<string, Prefab> Prefabs { get; } = new Dictionary<string, Prefab>();

        public IEntity Construct(string Name)
        {
            return Prefabs[Name].Construct();
        }
    }
}
