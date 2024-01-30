using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Framework.Constants
{
    public class CachingConstants
    {
        public class Prefix
        {
            public const string IDEMPOTENCY_KEY = "idempotency-key:";

        }
        public const int DEFAULT_CACHE_DURATION = 300;
    }
}
