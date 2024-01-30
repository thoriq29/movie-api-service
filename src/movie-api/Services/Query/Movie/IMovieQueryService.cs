using System.Collections.Generic;
using System.Threading.Tasks;
using Movie.Api.Dto.Movie;
using Service.Core.Interfaces.Framework;

namespace Movie.Api.Services.Query.Movie
{
    public interface IMovieQueryService
    {
        public Task<IServiceApiResult<List<MovieDto>>> GetListMovie();
        public Task<IServiceApiResult<MovieDto>> GetMovieDetail(long genreId);
    }
}
