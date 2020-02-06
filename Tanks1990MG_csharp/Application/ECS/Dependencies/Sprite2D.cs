using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tanks1990MG_csharp.Application.ECS.Dependencies
{
    class Sprite2D : IAutoDraw
    {
        public Vector3 Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector3 Rotation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector3 Scale { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
