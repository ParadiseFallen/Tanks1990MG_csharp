using EMCS.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990MG_csharp.Application.GameEntityes.Solutions.Fabric
{
    public class BuildingWrap
    {
        #region Data
        public EntityBuilder Builder { get; }
        private IGameEntity currentEntity;
        #endregion
        #region Events
        public event Action<object, IGameEntity> EntityConstruct;
        #endregion
        #region Data accesor
        public IGameEntity Resault { get { EntityConstruct?.Invoke(this, currentEntity); return currentEntity; } }
        #endregion
        #region Ctors
        public BuildingWrap(EntityBuilder Builder)
        {
            this.Builder = Builder;
        }
        #endregion
        #region Methods
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
