using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Movie.Api.Dto.Movie;
using Movie.Api.Dto.UserReview;
using Movie.Common.Services.Movie;
using Movie.Common.Services.User;
using Movie.Common.Services.UserReview;
using Service.Core.Interfaces.Framework;
using Service.Core.Interfaces.Log;

namespace Movie.Api.Services.Query.UserReview
{
    public class UserReviewQueryService : IUserReviewQueryService
    {
        private readonly ICoreLogger _logger;
        private readonly IErrorFactory _errorFactory;
        private readonly IServiceResultTemplate _serviceResultTemplate;
        private readonly IUserReviewService _userReviewService;
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public UserReviewQueryService(
            ICoreLogger logger,
            IErrorFactory errorFactory,
            IServiceResultTemplate serviceResultTemplate,
            IUserReviewService userReviewService,
            IUserService userService,
            IMovieService movieService,
            IMapper mapper
        )
        {
            _logger = logger;
            _errorFactory = errorFactory;
            _serviceResultTemplate = serviceResultTemplate;
            _userReviewService = userReviewService;
            _userService = userService;
            _movieService = movieService;
            _mapper = mapper;
        }

        public async Task<IServiceApiResult<UserReviewDto>> GetReviewDetail(UserReviewGetRequestDto dto)
        {
            try
            {
                var userData = await _userService.Find(it => it.AccountId == dto.AccountId && it.DeletedDate == null);
                if (userData == null)
                {
                    return _serviceResultTemplate.NotFound<UserReviewDto>("User data not found");
                }

                var userReview = await _userReviewService.Find(it => it.UserId == userData.ID && it.MovieId == dto.MovieId);
                if (userReview == null)
                {
                    return _serviceResultTemplate.NotFound<UserReviewDto>("User review not found");
                }

                var movie = await _movieService.Find(userReview.MovieId);
                if (movie == null)
                {
                    return _serviceResultTemplate.NotFound<UserReviewDto>("Associated movie not found");
                }

                var userReviewDto = _mapper.Map<UserReviewDto>(userReview);
                userReviewDto.Movie = _mapper.Map<MovieDto>(movie);
                userReviewDto.UserName = userData.Username;

                return _serviceResultTemplate.SuccessWithData(userReviewDto);
            }
            catch (Exception ex)
            {
                return _serviceResultTemplate.InternalServerError<UserReviewDto>($"An error occurred while retrieving user review detail {ex.Message}");
            }
        }

        public async Task<IServiceApiResult<List<UserReviewDto>>> GetReviewList(string accountId)
        {
            try
            {
                var userData = await _userService.Find(it => it.AccountId == accountId && it.DeletedDate == null);
                if (userData == null)
                {
                    return _serviceResultTemplate.NotFound<List<UserReviewDto>>("User data not found");
                }

                var userReviews = await _userReviewService.FindMany(it => it.UserId == userData.ID && it.DeletedDate == null);
                var userReviewDtos = _mapper.Map<List<UserReviewDto>>(userReviews);

                foreach (var userReviewDto in userReviewDtos)
                {
                    var movie = await _movieService.Find(userReviewDto.MovieId);
                    if (movie != null)
                    {
                        userReviewDto.Movie = _mapper.Map<MovieDto>(movie);
                        userReviewDto.UserName = userData.Username;
                    }
                }

                return _serviceResultTemplate.SuccessWithData(userReviewDtos);
            }
            catch (Exception ex)
            {
                return _serviceResultTemplate.InternalServerError<List<UserReviewDto>>($"An error occurred while retrieving user review list {ex.Message}");
            }
        }
    }
}
