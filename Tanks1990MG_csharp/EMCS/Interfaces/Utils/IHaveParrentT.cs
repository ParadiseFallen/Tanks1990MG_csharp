using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMCS.Interfaces
{
    public interface IHaveParent<T>
    {
        T ParentEntity { get; set; }
    }
}
