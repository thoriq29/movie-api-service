using Service.Core.Interfaces.Service;
using Movie.Common.Models.Movie;
using System.Threading.Tasks;
using Movie.Common.Models.Genre;

namespace Movie.Common.Services.Genre
{
    public interface IGenreService : IBaseService<GenreModel>
    {
    }
}
