using Service.Core.Interfaces.Repository;
using Movie.Common.Models.Movie;
using Movie.Common.Models.Genre;

namespace Movie.Common.Repositories.GenreRepository
{
    public interface IGenreRepository : IBaseRepository<GenreModel>
    {
    }
}
