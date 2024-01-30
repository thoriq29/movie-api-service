using Service.Core.Interfaces.Service;
using Movie.Common.Models.Movie;
using System.Threading.Tasks;
using Movie.Common.Models.User;

namespace Movie.Common.Services.User
{
    public interface IUserService : IBaseService<UserModel>
    {
    }
}
