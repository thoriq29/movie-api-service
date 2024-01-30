using System.Threading.Tasks;
using Movie.Api.Dto.Genre;
using Movie.Api.Dto.Movie;
using Movie.Api.Dto.UserReview;
using Service.Core.Interfaces.Framework;

namespace Movie.Api.Services.Command.UserReview
{
    public interface IUserReviewCommandService
    {
        public Task<IServiceApiResult<string>> PostReview(UserReviewRequestDto dto);
        public Task<IServiceApiResult<string>> EditReview(UserReviewRequestDto dto);
        public Task<IServiceApiResult<string>> DeleteReview(UserReviewGetRequestDto dto);
    }
}