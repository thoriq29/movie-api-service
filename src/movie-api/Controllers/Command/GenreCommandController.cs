using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.Api.Dto.Genre;
using Movie.Api.Services.Command.Genre;
using Service.Core.Framework.Responses;
using Service.Core.Interfaces.Framework;
using Service.Core.Log.Errors;
// using Movie.Api.Services.Command.Movie;

namespace Movie.Api.Controllers.Command.Genre
{
    [ApiController]
    [Route("admin/genre")]
    [ApiVersion("1.0")]
    [Authorize(Policy = "HasAuthorization")]
    public class GenreCommandController : ControllerBase
    {
        private readonly IServiceWrapper _serviceWrapper;
        private readonly IGenreCommandService _genreCommandService;

        public GenreCommandController(IServiceWrapper serviceWrapper,
            IGenreCommandService genreCommandService
            )
        {
            _serviceWrapper = serviceWrapper;
            _genreCommandService = genreCommandService;
        }

        [HttpPost]
        [Route("create")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BasicResponse), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> CreateGenre([FromBody] GenrePostRequestDto dto)
        {
            return await _serviceWrapper.CallServices(_genreCommandService.CreateGenre, dto);
        }

        [HttpPut]
        [Route("edit/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BasicResponse), 200)]
        [ProducesResponseType(typeof(Error), 400)]
        public async Task<IActionResult> EditGenre(long id, [FromBody] GenrePutRequestDto dto)
        {
            dto.Id = id;
            return await _serviceWrapper.CallServices(_genreCommandService.UpdateGenre, dto);
        }
    }
}
