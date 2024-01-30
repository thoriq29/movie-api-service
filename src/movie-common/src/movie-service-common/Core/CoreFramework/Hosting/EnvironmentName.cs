using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Framework.Hosting
{
    public static class EnvironmentName
    {
        public static readonly string Debug = nameof(Debug);
        public static readonly string Development = nameof(Development);
        public static readonly string Release = nameof(Release);
        public static readonly string Sandbox = nameof(Sandbox);
        public static readonly string Staging = nameof(Staging);
        public static readonly string Testing = nameof(Testing);
        public static readonly string Review = nameof(Review);
    }
}
