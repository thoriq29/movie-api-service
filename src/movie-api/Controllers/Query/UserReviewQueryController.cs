using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.Api.Dto.UserReview;
using Movie.Api.Services.Query.UserReview;
using Service.Core.Framework.Responses;
using Service.Core.Interfaces.Framework;
using Service.Core.Log.Errors;
using Service.Utilities;

namespace Movie.Api.Controllers.Query.UserReview
{
    [ApiController]
    [Route("user-review")]
    [ApiVersion("1.0")]
    [Authorize]
    public class UserReviewQueryQueryController : ControllerBase
    {
        private readonly IServiceWrapper _serviceWrapper;
        private readonly IUserReviewQueryService _userReviewQueryService;

        public UserReviewQueryQueryController(IServiceWrapper serviceWrapper,
            IUserReviewQueryService userReviewQueryService
            )
        {
            _serviceWrapper = serviceWrapper;
            _userReviewQueryService = userReviewQueryService;
        }

        [HttpGet]
        [Route("list")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BasicResponse), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> GetReviewList()
        {
            var userId = JWTHttpContext.GetUser(HttpContext).FindFirstValue("sub");
            return await _serviceWrapper.CallServices(_userReviewQueryService.GetReviewList, userId);
        }

        [HttpGet]
        [Route("detail/{movieId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BasicResponse), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> GetReviewDetail(long movieId)
        {
            var userId = JWTHttpContext.GetUser(HttpContext).FindFirstValue("sub");
            var dto = new UserReviewGetRequestDto()
            {
                AccountId = userId,
                MovieId = movieId
            };
            return await _serviceWrapper.CallServices(_userReviewQueryService.GetReviewDetail, dto);
        }
    }
}
