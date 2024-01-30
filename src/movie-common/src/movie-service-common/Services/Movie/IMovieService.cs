using Service.Core.Interfaces.Service;
using Movie.Common.Models.Movie;
using System.Threading.Tasks;

namespace Movie.Common.Services.Movie
{
    public interface IMovieService : IBaseService<MovieModel>
    {
    }
}
