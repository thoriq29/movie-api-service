using Service.Core.Interfaces.Repository;
using Movie.Common.Models.Movie;

namespace Movie.Common.Repositories.MovieRepository
{
    public interface IMovieRepository : IBaseRepository<MovieModel>
    {
    }
}
