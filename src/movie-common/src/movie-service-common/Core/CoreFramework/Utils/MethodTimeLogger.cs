using Service.Core.Interfaces.Log;
using System.Reflection;
using System;

namespace Service.Core.Framework.Utils
{
    public static class MethodTimeLogger
    {
        private static ICoreLogger coreLogger;
        public static ICoreLogger CoreLogger
        {
            get
            {
                return coreLogger;
            }
            set
            {
                coreLogger = value;
            }
        }

        public static void Log(MethodBase methodBase, TimeSpan elapsed, string message)
        {
            //Write to VS Debug Output window, logger, etc...
            if (methodBase is not null)
            {
                var declaringTypeName = methodBase.DeclaringType is not null ? methodBase.DeclaringType.Name : string.Empty;
                coreLogger.LogInformation($"{declaringTypeName}.{methodBase.Name} Time Ms: {elapsed} - {message}");
            }
            else
            {
                coreLogger.LogInformation($"Time Ms: {elapsed} - {message}");
            }
        }
    }
}
