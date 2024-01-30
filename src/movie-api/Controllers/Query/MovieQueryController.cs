using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.Api.Services.Query.Movie;
using Service.Core.Framework.Responses;
using Service.Core.Interfaces.Framework;
using Service.Core.Log.Errors;
// using Movie.Api.Services.Query.Movie;

namespace Movie.Api.Controllers.Query.Movie
{
    [ApiController]
    [Route("movie")]
    [ApiVersion("1.0")]
    [Authorize]
    public class MovieQueryController : ControllerBase
    {
        private readonly IMovieQueryService _handlerQueryService;
        private readonly IServiceWrapper _serviceWrapper;
        public MovieQueryController(
            IMovieQueryService handlerQueryService, 
            IServiceWrapper serviceWrapper)
        {
            _handlerQueryService = handlerQueryService;
            _serviceWrapper = serviceWrapper;
        }

        [HttpGet]
        [Route("list")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BasicResponse), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> GetList()
        {
            return await _serviceWrapper.CallServices(_handlerQueryService.GetListMovie);
        }

        [HttpGet]
        [Route("detail/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BasicResponse), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> GetDetail(long id)
        {
            return await _serviceWrapper.CallServices(_handlerQueryService.GetMovieDetail, id);
        }
    }
}
