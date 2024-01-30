using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.Api.Dto.Genre;
using Movie.Api.Dto.Movie;
using Movie.Api.Services.Command.Movie;
using Service.Core.Framework.Responses;
using Service.Core.Interfaces.Framework;
using Service.Core.Log.Errors;

namespace Movie.Api.Controllers.Command.Movie
{
    [ApiController]
    [Route("admin/movie")]
    [ApiVersion("1.0")]
    [Authorize(Policy = "HasAuthorization")]
    public class MovieCommandController : ControllerBase
    {
        private readonly IServiceWrapper _serviceWrapper;
        private readonly IMovieCommandService _movieCommandService;

        public MovieCommandController(IServiceWrapper serviceWrapper,
            IMovieCommandService movieCommandService
            )
        {
            _serviceWrapper = serviceWrapper;
            _movieCommandService = movieCommandService;
        }

        [HttpPost]
        [Route("create")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BasicResponse), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> CreateMovie([FromBody] MoviePostRequestDto dto)
        {
            return await _serviceWrapper.CallServices(_movieCommandService.CreateMovie, dto);
        }

        [HttpPut]
        [Route("edit/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BasicResponse), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> EditMovie(long id, [FromBody] MoviePutRequestDto dto)
        {
            dto.Id = id;
            return await _serviceWrapper.CallServices(_movieCommandService.UpdateMovie, dto);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BasicResponse), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> DeleteMovie(long id)
        {
            return await _serviceWrapper.CallServices(_movieCommandService.DeleteMovie, id);
        }
    }
}
