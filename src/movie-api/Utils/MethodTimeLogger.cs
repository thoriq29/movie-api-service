using Service.Core.Interfaces.Log;
using System.Reflection;
using System;

namespace Movie.Api.Utils
{
    public static class MethodTimeLogger
    {
        public static ICoreLogger CoreLogger;

        public static void Log(MethodBase methodBase, TimeSpan elapsed, string message)
        {
            //Write to VS Debug Output window, logger, etc...
            CoreLogger.LogInformation($"{methodBase.DeclaringType!.Name}.{methodBase.Name} Time Ms: {elapsed} - {message}");
        }
    }
}