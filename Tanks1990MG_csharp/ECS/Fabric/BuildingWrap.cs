using EMCS.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Fabric
{
    public class BuildingWrap
    {
        #region Data
        public EntityBuilder Builder { get; }

        private IEntity currentEntity;
        #endregion

        #region Events
        public event Action<object, IEntity> EntityConstruct;
        #endregion

        #region Data accesor
        public IEntity Resault { get { EntityConstruct?.Invoke(this, currentEntity); return currentEntity; } }
        #endregion

        #region Ctors
        public BuildingWrap(EntityBuilder Builder)
        {
            this.Builder = Builder;
        }
        #endregion
        #region Methods
        public BuildingWrap AutoBuild(DecorationChain chain)
        {


            return this;
        }
        public BuildingWrap StartBuild(string ProviderName)
        {
            currentEntity = Builder.EntityProvider[ProviderName].Get();
            return this;
        }
        public BuildingWrap Decorate(string DecoratorName)
        {
            if (currentEntity is null)
                throw new Exception("Construct entity first!");
            var d = Builder.Decorators[DecoratorName];
            d.Builder = this.Builder;
            d.Decorate(currentEntity);
            return this;
        }
        //public BuildingWrap RunChain()
        //{

        //    return this;
        //}
        #endregion
    }
}
