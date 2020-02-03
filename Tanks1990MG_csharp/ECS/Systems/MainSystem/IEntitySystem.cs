using EMCS.Interfaces;
using EMCS.Interfaces.Entity;
using EMCS.Interfaces.System;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMCS.Systems.MainSystem
{
    public interface IEntitySystem 
    {
        ISystemStorage Systems { get; }
        IChildContainer<IEntity> Entities { get; }
    }
}