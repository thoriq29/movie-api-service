using System.Threading.Tasks;
using Movie.Api.Dto.Genre;
using Movie.Api.Dto.Movie;
using Service.Core.Interfaces.Framework;

namespace Movie.Api.Services.Command.Genre
{
    public interface IGenreCommandService
    {
        public Task<IServiceApiResult<string>> CreateGenre(GenrePostRequestDto dto);
        public Task<IServiceApiResult<string>> UpdateGenre(GenrePutRequestDto dto);
    }
}
