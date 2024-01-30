
using System.Collections.Generic;
using System.Threading.Tasks;
using Movie.Api.Dto.Genre;
using Movie.Api.Dto.Movie;

namespace Movie.Api.Services.Caching
{
    public interface IMovieCachingService
    {
        public Task<List<GenreDto>> GetListGenreDto();
        public Task<bool> SetGenreListToCache(List<GenreDto> data);

        public Task<List<MovieDto>> GetListMovieDto();
        public Task<bool> SetMovieListToCache(List<MovieDto> data);
    }
}