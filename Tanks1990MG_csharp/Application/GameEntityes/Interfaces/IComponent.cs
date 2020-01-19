using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990MG_csharp.Application.GameEntityes.Interfaces
{
    public interface IComponent<T>
    {
        T Parent{ get; set; }
    }
}
