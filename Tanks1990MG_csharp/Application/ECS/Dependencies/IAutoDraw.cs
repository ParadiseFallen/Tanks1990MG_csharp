using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990MG_csharp.Application.ECS.Dependencies
{
    interface IAutoDraw  : IUpdateable
    {
        int ZPoz { get; set; }
        Vector3 Position { get; set; }
        Vector3 Rotation{ get; set; }
        Vector3 Scale { get; set; }
        void Draw(DrawData drawData);
    }
}
