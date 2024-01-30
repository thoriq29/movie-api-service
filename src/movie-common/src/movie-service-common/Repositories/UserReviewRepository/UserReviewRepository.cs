using Service.Core.Interfaces.Log;
using Service.Core.MySql;
using Movie.Common.Models.Movie;
using Movie.Common.Models.UserReview;

namespace Movie.Common.Repositories.UserReviewRepository
{
    internal sealed class UserReviewRepository : BaseMySqlRepository<UserReviewModel>, IUserReviewRepository
    {
        public UserReviewRepository(BaseDbContext dbContext, ICoreLogger logger, IErrorFactory errorList) : base(dbContext, logger, errorList)
        {

        }
    }
}
