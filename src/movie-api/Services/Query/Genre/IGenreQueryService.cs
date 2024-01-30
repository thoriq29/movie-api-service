using System.Collections.Generic;
using System.Threading.Tasks;
using Movie.Api.Dto.Genre;
using Service.Core.Interfaces.Framework;

namespace Movie.Api.Services.Query.Genre
{
    public interface IGenreQueryService
    {
        public Task<IServiceApiResult<List<GenreDto>>> GetListGenre();
        public Task<IServiceApiResult<GenreDto>> GetGenreDetail(long genreId);
    }
}
