using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.Api.Dto.UserReview;
using Movie.Api.Services.Command.User;
using Movie.Api.Services.Command.UserReview;
using Service.Core.Framework.Responses;
using Service.Core.Interfaces.Framework;
using Service.Core.Log.Errors;
using Service.Utilities;

namespace Movie.Api.Controllers.Command.User
{
    [ApiController]
    [Route("user")]
    [ApiVersion("1.0")]
    [Authorize]
    public class UserCommandController : ControllerBase
    {
        private readonly IServiceWrapper _serviceWrapper;
        private readonly IUserCommandService _userCommandService;

        public UserCommandController(IServiceWrapper serviceWrapper,
            IUserCommandService userCommandService
            )
        {
            _serviceWrapper = serviceWrapper;
            _userCommandService = userCommandService;
        }

        [HttpPost]
        [Route("register")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BasicResponse), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> Register()
        {
            var userId = JWTHttpContext.GetUser(HttpContext).FindFirstValue("sub");
            return await _serviceWrapper.CallServices(_userCommandService.Regiser, userId);
        }
    }
}
