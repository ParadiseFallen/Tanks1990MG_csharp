using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990MG_csharp.Application
{
    [Serializable]
    class ApplicationSettings
    {
        static private ApplicationSettings _Settings;
        public static ApplicationSettings Settings{ get { if (_Settings is null) _Settings = new ApplicationSettings(); return _Settings; }}

        //public int MyProperty { get; set; }







    }
}
