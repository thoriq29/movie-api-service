using Microsoft.AspNetCore.Mvc;
using Service.Core.Interfaces.Log;

namespace Service.Core.Interfaces.Framework
{
    public interface IInvalidModelStateError
    {
        public BadRequestObjectResult CustomErrorResponse(ActionContext actionContext);
    }
}
