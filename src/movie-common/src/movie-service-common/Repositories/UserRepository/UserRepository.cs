using Service.Core.Interfaces.Log;
using Service.Core.MySql;
using Movie.Common.Models.Movie;
using Movie.Common.Models.User;

namespace Movie.Common.Repositories.UserRepository
{
    internal sealed class UserRepository : BaseMySqlRepository<UserModel>, IUserRepository
    {
        public UserRepository(BaseDbContext dbContext, ICoreLogger logger, IErrorFactory errorList) : base(dbContext, logger, errorList)
        {

        }
    }
}
