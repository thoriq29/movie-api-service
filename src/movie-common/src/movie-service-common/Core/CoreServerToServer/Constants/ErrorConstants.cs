using Service.Core.Log.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServerToServer.Constants
{
    internal class ErrorConstants
    {
        public static readonly Error E118001 = new Error() { code = "118001", message = "Get Discovery Document Async Faield" };
        public static readonly Error E118002 = new Error() { code = "118002", message = "Failed to get token because an exception has occurred" };
        public static readonly Error E118003 = new Error() { code = "118003", message = "Failed to send request to communication service" };
        public static readonly Error E118004 = new Error() { code = "118004", message = "Request Access Token Failed" };
        public static readonly Error E118005 = new Error() { code = "118005", message = "Failed to get request to mythic account" };
        public static readonly Error E118006 = new Error() { code = "118006", message = "Service Account request but not providing accessToken" };
        public static readonly Error E118007 = new Error() { code = "118007", message = "Service Account request but not providing clientId" };
    }
}
