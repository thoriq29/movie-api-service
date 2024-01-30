using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Service.Core.Framework.Responses;
using Service.Core.Interfaces.Framework;
using Service.Core.Interfaces.Log;

namespace Service.Core.Framework.Utils
{
    public class InvalidModelStateError : IInvalidModelStateError
    {
        IErrorFactory _error;

        public InvalidModelStateError(IErrorFactory errorFactory)
        {
            _error = errorFactory;
        }
        public BadRequestObjectResult CustomErrorResponse(ActionContext actionContext)
        {
            var errors = actionContext.ModelState
                .Where(modelError => modelError.Value.Errors.Count > 0)
                .Select(modelError =>
                    _error.CreateError(_error.CreateInsufficientParams().code, 
                    modelError.Value.Errors.FirstOrDefault().ErrorMessage, 
                    null, null, null))
                .ToArray();

            var errResponse = new BasicResponse()
            {
                Error = _error.CreateError(_error.CreateInsufficientParams().code, "Invalid model state!", null, errors, null),
                Message = "failed",
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };

            return new BadRequestObjectResult(errResponse);
        }
    }
}
