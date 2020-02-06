using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks1990MG_csharp.Application.InputMG.Solutions
{
    [Serializable]
    class KeyMetadata
    {
        public KeyMetadata(List<string> ActionF = null)
        {
            this.ActionF = ActionF;
            if (ActionF == null) ActionF = new List<string>();
        }

        public string Description { get; set; }
        public List<string> ActionF { get; set; }

        #region Triger
        public string Triger { get; set; }
        public string TrigerCode { get; set; }
        #endregion
        #region Repeating
        public bool RepeatDelayEnabled { get; set; }
        public int RepeatDelayMS { get; set; }
        #endregion
    }
}
