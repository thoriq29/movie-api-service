using Service.Core.Interfaces.Log;
using Movie.Common.Models.Movie;
using Movie.Common.Repositories.MovieRepository;
using Service.Core.Framework.Services;
using System;
using System.Threading.Tasks;
using Movie.Common.Models.UserReview;
using Movie.Common.Repositories.UserReviewRepository;

namespace Movie.Common.Services.UserReview
{
    public sealed class UserReviewService : BaseService<UserReviewModel>, IUserReviewService
    {
        public UserReviewService(IUserReviewRepository genreRepository, ICoreLogger logger, IErrorFactory errorFactory) : base(genreRepository, logger, errorFactory)
        {

        }
    }
}
