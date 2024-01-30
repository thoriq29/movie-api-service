using Service.Core.Interfaces.Framework;
using Service.Core.Interfaces.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Log.Errors
{
    public class Error : IError
    {
        public string code { get; set; }

        public string message { get; set; }

        public string target { get; set; }

        public object[] details { get; set; }

        public IInnerError innererror { get; set; }
    }
}
