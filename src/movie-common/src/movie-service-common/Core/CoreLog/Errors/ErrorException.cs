using Service.Core.Interfaces.Log;
using System;

namespace Service.Core.Log.Errors
{
    public class ErrorException : Exception, IErrorException
    {
        public IError Error { get; set; }
        public ErrorException(IError error)
        {
            Error = error;
        }
    }
}
