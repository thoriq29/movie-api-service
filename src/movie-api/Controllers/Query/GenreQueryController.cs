using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.Api.Services.Query.Genre;
using Service.Core.Framework.Responses;
using Service.Core.Interfaces.Framework;
using Service.Core.Log.Errors;
// using Movie.Api.Services.Query.Movie;

namespace Movie.Api.Controllers.Query.Movie
{
    [ApiController]
    [Route("genre")]
    [ApiVersion("1.0")]
    [Authorize]
    public class GenreQueryController : ControllerBase
    {
        private readonly IGenreQueryService _handlerQueryService;
        private readonly IServiceWrapper _serviceWrapper;
        public GenreQueryController(
            IGenreQueryService handlerQueryService, 
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
            return await _serviceWrapper.CallServices(_handlerQueryService.GetListGenre);
        }

        [HttpGet]
        [Route("detail/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BasicResponse), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> GetDetail(long id)
        {
            return await _serviceWrapper.CallServices(_handlerQueryService.GetGenreDetail, id);
        }
    }
}
