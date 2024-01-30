using System.Collections.Generic;
using System.Threading.Tasks;
using Movie.Api.Dto.Movie;
using Movie.Api.Dto.UserReview;
using Service.Core.Interfaces.Framework;

namespace Movie.Api.Services.Query.UserReview
{
    public interface IUserReviewQueryService
    {
        public Task<IServiceApiResult<List<UserReviewDto>>> GetReviewList(string accountId);
        public Task<IServiceApiResult<UserReviewDto>> GetReviewDetail(UserReviewGetRequestDto dto);
    }
}
