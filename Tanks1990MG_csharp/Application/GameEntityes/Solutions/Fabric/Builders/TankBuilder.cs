using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.GameEntityes.Interfaces;

namespace Tanks1990MG_csharp.Application.GameEntityes.Solutions.Fabric.Builders
{
    class TankBuilder : IEntityBuilder
    {
        //public IGameEntity Build()
        //{
        //    return new GameEntity
        //    {
        //        RendererModel = new ObjectRendererSprite() { Source = new Sprite(GraphicManager.Instance.Textures["TankA"]) },
        //        PhisycModel = new ObjectPhisycModel2d() { AccelerationAttenuationRate0_100 = 20 }
        //    };
        //}

        //public IGameEntity Build(EventArgs arg)
        //{
        //    throw new NotImplementedException();
        //}
        public IGameEntity Build()
        {


            return null;
        }

        public IGameEntity Build(EventArgs arg)
        {
            throw new NotImplementedException();
        }
    }
}
