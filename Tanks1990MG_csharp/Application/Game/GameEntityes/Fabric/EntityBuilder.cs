using EMCS.Interfaces.Entity;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.Game.GameEntityes.Fabric.Decorators;
using Tanks1990MG_csharp.Application.GameEntityes.Interfaces;
using Tanks1990MG_csharp.Application.GameEntityes.Solutions.Fabric;
using Tanks1990MG_csharp.Application.GameEntityes.Solutions.Fabric.Builders;
using Tanks1990MG_csharp.Application.GameEntityes.Solutions.Fabric.Decorators;
using Tanks1990MG_csharp.Application.GameEntityes.Solutions.Fabric.EntityProvider;
using Tanks1990MG_csharp.Application.Providers.Interfaces;

namespace Tanks1990MG_csharp.Application.GameEntityes.Solutions
{
    //создание любой сущности происходит через фабоику
    public class EntityBuilder
    {
        #region Instancing
        static private EntityBuilder _BuilderInstance;
        public static EntityBuilder BuilderInstance { get { if (_BuilderInstance is null) _BuilderInstance = new EntityBuilder(); return _BuilderInstance; } }
        #endregion

        /*
         Надо создать пул компонентов, а так же элементов.
         Список провайдеров пустых экземпляров обьектов
             */
        public Dictionary<string, IEntityDecorator> Decorators { get; } = new Dictionary<string, IEntityDecorator>();
        public Dictionary<string, IProvider<IGameEntity>> EntityProvider { get; } = new Dictionary<string, IProvider<IGameEntity>>();
        public ContentManager Content { get; set; }
        private EntityBuilder()
        {
            //push providers
            EntityProvider.Add("CustomEntity", new CustomEntityProvider());
            //push decorators
            Decorators.Add("AutoGUID", new GUID_Decorator());
            Decorators.Add("TankTextureSprite", new RendererDecorator());
           
        }

        public BuildingWrap GetWrap() {
            return new BuildingWrap(this) { };
        }

    }
}
