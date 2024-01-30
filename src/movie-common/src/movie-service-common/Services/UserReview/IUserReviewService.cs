using Service.Core.Interfaces.Service;
using Movie.Common.Models.Movie;
using System.Threading.Tasks;
using Movie.Common.Models.UserReview;

namespace Movie.Common.Services.UserReview
{
    public interface IUserReviewService : IBaseService<UserReviewModel>
    {
    }
}
