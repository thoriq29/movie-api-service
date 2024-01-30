using Service.Core.Interfaces.Framework;
using Service.Core.Interfaces.Log;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.Log.Logger
{
    public class CoreLogger : ICoreLogger
    {
        private readonly ILogger _logger;

        public CoreLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void LogInformation(string informationMessage,
            [CallerMemberName] string methodName = "")
        {
            _logger.Information($"[{methodName}]" +
                                  $"{informationMessage}");
        }

        public void LogWarning(string message = "",
            string inputParams = "",
            [CallerMemberName] string methodName = "")
        {
            _logger.Warning(
                $@"[{methodName}]{(message != "" ? $"[{message}]\n" : "")}{(inputParams != "" ? $"{inputParams}\n" : "")}"
            );
        }

        public void LogWarning(IError error, string message = "",
            string inputParams = "",
            [CallerMemberName] string methodName = "")
        {
            _logger.Warning(
                $@"[{methodName}]{(error.code != "" ? $"[{error.code}]" : "")}{(error.message != "" ? $"[{error.message}]" : "")}{(message != "" ? $"[{message}]\n" : "")}{(inputParams != "" ? $"{inputParams}\n" : "")}"
            );
        }

        public void LogError(IError error, string message = "",
            string inputParams = "", Exception? exception = null,
            [CallerMemberName] string methodName = "")
        {
            _logger.Error(
                $@"[{methodName}]{(error.code != "" ? $"[{error.code}]" : "")}{(error.message != "" ? $"[{error.message}]" : "")}{(message != "" ? $"[{message}]\n" : "")}{(inputParams != "" ? $"{inputParams}\n" : "")}{(exception != null ? GenerateErrorMessage(exception) : "")}"
            );
        }

        public void LogError(IError error,
            [CallerMemberName] string methodName = "")
        {
            _logger.Error($"[{methodName}]" +
                            $"[{error.code}]" +
                            $"[{error.message}]");
        }

        public void LogError(IError error, Exception exception,
            [CallerMemberName] string methodName = "")
        {
            _logger.Error($"[{methodName}]" +
                            $"[{error.code}]" +
                            $"[{error.message}]" +
                            GenerateErrorMessage(exception));
        }

        public void LogCritical(IError error, string message = "",
            string inputParams = "", Exception? exception = null,
            [CallerMemberName] string methodName = "")
        {
            _logger.Fatal(
                $@"[{methodName}]{(error.code != "" ? $"[{error.code}]" : "")}{(error.message != "" ? $"[{error.message}]" : "")}{(message != "" ? $"[{message}]\n" : "")}{(inputParams != "" ? $"{inputParams}\n" : "")}{(exception != null ? GenerateErrorMessage(exception) : "")}"
            );
        }

        public void LogCritical(IError error,
            [CallerMemberName] string methodName = "")
        {
            _logger.Fatal($"[{methodName}]" +
                            $"[{error.code}]" +
                            $"[{error.message}]");
        }

        public void LogCritical(IError error, Exception exception,
            [CallerMemberName] string methodName = "")
        {
            _logger.Fatal($"[{methodName}]" +
                            $"[{error.code}]" +
                            $"[{error.message}]" +
                            GenerateErrorMessage(exception));
        }

        private string GenerateErrorMessage(Exception exception)
        {
            return $"Messsage: {exception.Message}, Inner: {exception.InnerException}, StackTrace: {exception.StackTrace}, Source: {exception.Source}";
        }
    }
}
