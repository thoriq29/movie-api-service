using Service.Core.Interfaces.Log;
using Service.Core.MySql;
using Movie.Common.Models.Movie;

namespace Movie.Common.Repositories.MovieRepository
{
    internal sealed class MovieRepository : BaseMySqlRepository<MovieModel>, IMovieRepository
    {
        public MovieRepository(BaseDbContext dbContext, ICoreLogger logger, IErrorFactory errorList) : base(dbContext, logger, errorList)
        {

        }
    }
}
