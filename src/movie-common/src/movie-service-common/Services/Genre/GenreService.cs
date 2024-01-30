using Service.Core.Interfaces.Log;
using Movie.Common.Models.Movie;
using Movie.Common.Repositories.MovieRepository;
using Service.Core.Framework.Services;
using System;
using System.Threading.Tasks;
using Movie.Common.Models.Genre;
using Movie.Common.Repositories.GenreRepository;

namespace Movie.Common.Services.Genre
{
    public sealed class GenreService : BaseService<GenreModel>, IGenreService
    {
        public GenreService(IGenreRepository genreRepository, ICoreLogger logger, IErrorFactory errorFactory) : base(genreRepository, logger, errorFactory)
        {

        }
    }
}
