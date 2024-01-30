using Service.Core.Interfaces.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Framework.Configurations
{
    public class SwaggerInfoOption : ISwaggerInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
