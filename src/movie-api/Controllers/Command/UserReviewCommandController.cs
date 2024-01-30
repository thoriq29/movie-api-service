using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.Api.Dto.UserReview;
using Movie.Api.Services.Command.UserReview;
using Service.Core.Framework.Responses;
using Service.Core.Interfaces.Framework;
using Service.Core.Log.Errors;
using Service.Utilities;

namespace Movie.Api.Controllers.Command.UserReview
{
    [ApiController]
    [Route("user-review")]
    [ApiVersion("1.0")]
    [Authorize]
    public class UserReviewCommandController : ControllerBase
    {
        private readonly IServiceWrapper _serviceWrapper;
        private readonly IUserReviewCommandService _userReviewCommandService;

        public UserReviewCommandController(IServiceWrapper serviceWrapper,
            IUserReviewCommandService userReviewCommandService
            )
        {
            _serviceWrapper = serviceWrapper;
            _userReviewCommandService = userReviewCommandService;
        }

        [HttpPost]
        [Route("movie/add")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BasicResponse), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> PostReview([FromBody] UserReviewRequestDto dto)
        {
            var userId = JWTHttpContext.GetUser(HttpContext).FindFirstValue("sub");
            dto.AccountId = userId;
            return await _serviceWrapper.CallServices(_userReviewCommandService.PostReview, dto);
        }

        [HttpPut]
        [Route("movie/edit/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BasicResponse), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> EditReview(long id, [FromBody] UserReviewRequestDto dto)
        {
            var userId = JWTHttpContext.GetUser(HttpContext).FindFirstValue("sub");
            dto.AccountId = userId;
            return await _serviceWrapper.CallServices(_userReviewCommandService.EditReview, dto);
        }

        [HttpDelete]
        [Route("movie/delete/{movieId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BasicResponse), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> DeleteReview(long movieId)
        {
            var userId = JWTHttpContext.GetUser(HttpContext).FindFirstValue("sub");
            var dto = new UserReviewGetRequestDto()
            {
                AccountId = userId,
                MovieId = movieId
            };
            return await _serviceWrapper.CallServices(_userReviewCommandService.DeleteReview, dto);
        }
    }
}
