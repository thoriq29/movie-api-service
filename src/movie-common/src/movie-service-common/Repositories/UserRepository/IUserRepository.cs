using Service.Core.Interfaces.Repository;
using Movie.Common.Models.Movie;
using Movie.Common.Models.User;

namespace Movie.Common.Repositories.UserRepository
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
    }
}
