using Service.Core.Log.Errors;

namespace Movie.Api.Utils
{
    internal static class ErrorUtils
    {
        #region Genre Validation
        internal static Error E123001(string detail, string methodName)
        {
            return new Error()
            {
                code = "123001",
                message = $"{methodName} genre failed because of invalid request payload. Detail : {detail}"
            };
        }

        internal static Error E123002(string detail, string methodName)
        {
            return new Error()
            {
                code = "123002",
                message = $"{methodName} genre failed because genre already exists. Detail : {detail}"
            };
        }

        internal static Error E123003(string detail, string methodName)
        {
            return new Error()
            {
                code = "123003",
                message = $"{methodName} genre failed because genre not found. Detail : {detail}"
            };
        }

        internal static readonly Error E123004 = new() { code = "124101", message = "Failed to create or edit genre because exception has occured." };
        #endregion
    }
}