using Service.Core.Interfaces.Log;
using Service.Core.MySql;
using Movie.Common.Models.Movie;
using Movie.Common.Models.Genre;

namespace Movie.Common.Repositories.GenreRepository
{
    internal sealed class GenreRepository : BaseMySqlRepository<GenreModel>, IGenreRepository
    {
        public GenreRepository(BaseDbContext dbContext, ICoreLogger logger, IErrorFactory errorList) : base(dbContext, logger, errorList)
        {

        }
    }
}
