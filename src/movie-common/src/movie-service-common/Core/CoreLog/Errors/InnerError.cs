using Service.Core.Interfaces.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Log.Errors
{
    public class InnerError : IInnerError
    {
        public string Code { get; set; }
        public IInnerError Innererror { get; set; }
    }
}
