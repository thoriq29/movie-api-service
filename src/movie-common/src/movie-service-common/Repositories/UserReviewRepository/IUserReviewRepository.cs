using Service.Core.Interfaces.Repository;
using Movie.Common.Models.Movie;
using Movie.Common.Models.UserReview;

namespace Movie.Common.Repositories.UserReviewRepository
{
    public interface IUserReviewRepository : IBaseRepository<UserReviewModel>
    {
    }
}
