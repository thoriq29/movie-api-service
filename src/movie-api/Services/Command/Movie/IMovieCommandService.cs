using System.Threading.Tasks;
using Movie.Api.Dto.Movie;
using Service.Core.Interfaces.Framework;

namespace Movie.Api.Services.Command.Movie
{
    public interface IMovieCommandService
    {
        public Task<IServiceApiResult<string>> CreateMovie(MoviePostRequestDto dto);
        public Task<IServiceApiResult<string>> UpdateMovie(MoviePutRequestDto dto);
        public Task<IServiceApiResult<string>> DeleteMovie(long movieId);
    }
}