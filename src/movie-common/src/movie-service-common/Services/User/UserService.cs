using Service.Core.Interfaces.Log;
using Movie.Common.Models.Movie;
using Movie.Common.Repositories.MovieRepository;
using Service.Core.Framework.Services;
using System;
using System.Threading.Tasks;
using Movie.Common.Models.User;
using Movie.Common.Repositories.UserRepository;

namespace Movie.Common.Services.User
{
    public sealed class UserService : BaseService<UserModel>, IUserService
    {
        public UserService(IUserRepository genreRepository, ICoreLogger logger, IErrorFactory errorFactory) : base(genreRepository, logger, errorFactory)
        {

        }
    }
}
