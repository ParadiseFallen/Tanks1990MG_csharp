using ECS.Fabric.Decorators;
using EMCS.Interfaces.Entity;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using Tanks1990MG_csharp.Application.Providers.Interfaces;

namespace ECS.Fabric
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
        public Dictionary<string, IProvider<IEntity>> EntityProvider { get; } = new Dictionary<string, IProvider<IEntity>>();
        public ContentManager Content { get; set; }
        private EntityBuilder()
        {
            //push providers
            EntityProvider.Add("CustomEntity", new CustomEntityProvider());
            //push decorators
            //Decorators.Add("AutoGUID", new GUID_Decorator());
            //Decorators.Add("TankTextureSprite", new RendererDecorator());
           
        }

        public BuildingWrap GetWrap() {
            return new BuildingWrap(this) { };
        }

    }
}
