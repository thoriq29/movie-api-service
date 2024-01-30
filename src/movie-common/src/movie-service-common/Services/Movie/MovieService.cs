using Service.Core.Interfaces.Log;
using Movie.Common.Models.Movie;
using Movie.Common.Repositories.MovieRepository;
using Service.Core.Framework.Services;
using System;
using System.Threading.Tasks;

namespace Movie.Common.Services.Movie
{
    public sealed class MovieService : BaseService<MovieModel>, IMovieService
    {
        public MovieService(IMovieRepository MovieRepository, ICoreLogger logger, IErrorFactory errorFactory) : base(MovieRepository, logger, errorFactory)
        {

        }

        //you can add your own method
        public async Task<MovieModel> FindByMovieName(string Moviename)
        {
            return await Find(Movie => Movie.Title == Moviename);
        }
    }
}
