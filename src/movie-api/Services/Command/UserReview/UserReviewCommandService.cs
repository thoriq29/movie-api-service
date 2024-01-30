
using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Movie.Api.Dto.Movie;
using Movie.Api.Dto.UserReview;
using Movie.Api.Services.Command.Movie;
using Movie.Api.Utils;
using Movie.Common.Models.Movie;
using Movie.Common.Models.UserReview;
using Movie.Common.Services.Genre;
using Movie.Common.Services.Movie;
using Movie.Common.Services.User;
using Movie.Common.Services.UserReview;
using Service.Core.Interfaces.Framework;
using Service.Core.Interfaces.Log;

namespace Movie.Api.Services.Command.UserReview
{
    public class UserReviewCommandService : IUserReviewCommandService
    {
        private readonly ICoreLogger _logger;
        private readonly IErrorFactory _errorFactory;
        private readonly IServiceResultTemplate _serviceResultTemplate;
        private readonly IValidator<UserReviewRequestDto> _requestValidator;
        private readonly IMovieService _movieService;
        private readonly IUserReviewService _userReviewService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserReviewCommandService(
            ICoreLogger logger,
            IErrorFactory errorFactory,
            IServiceResultTemplate serviceResultTemplate,
            IValidator<UserReviewRequestDto> requestValidator,
            IMovieService movieService,
            IUserReviewService userReviewService,
            IUserService userService,
            IMapper mapper
        )
        {
            _logger = logger;
            _errorFactory = errorFactory;
            _serviceResultTemplate = serviceResultTemplate;
            _requestValidator = requestValidator;
            _movieService = movieService;
            _userReviewService = userReviewService;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IServiceApiResult<string>> DeleteReview(UserReviewGetRequestDto dto)
        {
            try
            {
                if (dto.MovieId <= 0)
                {
                    return _serviceResultTemplate.BadRequest<string>("Movie not found");
                }
                // Check if the movie exists
                var movie = await _movieService.Find(dto.MovieId);
                if (movie == null || movie.IsDeleted())
                {
                    return _serviceResultTemplate.BadRequest<string>("Movie not found");
                }

                // Check if the user exists
                var user = await _userService.Find( it => it.AccountId == dto.AccountId);
                if (user == null || user.IsDeleted())
                {
                    return _serviceResultTemplate.BadRequest<string>("User not found");
                }

                var previousReview = await _userReviewService.Find(it => it.UserId == user.ID && it.MovieId == dto.MovieId);
                if (previousReview == null || previousReview.IsDeleted())
                {
                    return _serviceResultTemplate.BadRequest<string>("User Review not found or already deleted");
                }
                previousReview.DeletedDate = DateTime.Now;

                await _userReviewService.Update(previousReview);
                return _serviceResultTemplate.Success();
            }
            catch(Exception ex)
            {
                return _serviceResultTemplate.InternalServerError<string>($"An error occurred while delete the review {ex.Message}");
            }
        }

        public async Task<IServiceApiResult<string>> EditReview(UserReviewRequestDto dto)
        {
            try
            {
                // Validate input DTO
                var validationResult = await _requestValidator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    var errorMessage = string.Join(", ", validationResult.Errors);
                    _logger.LogInformation($"Validation error: {errorMessage}");
                    return _serviceResultTemplate.BadRequest<string>(errorMessage);
                }

                // Check if the movie exists
                var movie = await _movieService.Find(dto.MovieId);
                if (movie == null || movie.IsDeleted())
                {
                    return _serviceResultTemplate.BadRequest<string>("Movie not found");
                }

                // Check if the user exists
                var user = await _userService.Find( it => it.AccountId == dto.AccountId);
                if (user == null || user.IsDeleted())
                {
                    return _serviceResultTemplate.BadRequest<string>("User not found");
                }

                var previousReview = await _userReviewService.Find(it => it.UserId == user.ID && it.MovieId == dto.MovieId);
                if (previousReview == null || previousReview.IsDeleted())
                {
                    return _serviceResultTemplate.BadRequest<string>("User Review not found");
                }

                if (previousReview.ReviewText != dto.ReviewText)
                {
                    previousReview.ReviewText = dto.ReviewText;
                }

                if (previousReview.Rating != dto.Rating)
                {
                    previousReview.Rating = dto.Rating;
                }
                await _userReviewService.Update(previousReview);
                return _serviceResultTemplate.Success();
            }
            catch(Exception ex)
            {
                return _serviceResultTemplate.InternalServerError<string>($"An error occurred while edit the review {ex.Message}");
            }
        }

        public async Task<IServiceApiResult<string>> PostReview(UserReviewRequestDto dto)
        {
            try
            {
                // Validate input DTO
                var validationResult = await _requestValidator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    var errorMessage = string.Join(", ", validationResult.Errors);
                    _logger.LogInformation($"Validation error: {errorMessage}");
                    return _serviceResultTemplate.BadRequest<string>(errorMessage);
                }

                // Check if the movie exists
                var movie = await _movieService.Find(dto.MovieId);
                if (movie == null || movie.IsDeleted())
                {
                    return _serviceResultTemplate.BadRequest<string>("Movie not found");
                }

                // Check if the user exists
                var user = await _userService.Find( it => it.AccountId == dto.AccountId);
                if (user == null || user.IsDeleted())
                {
                    return _serviceResultTemplate.BadRequest<string>("User not found");
                }

                var previousReview = await _userReviewService.Find(it => it.UserId == user.ID && it.MovieId == dto.MovieId);
                if (previousReview != null && !previousReview.IsDeleted())
                {
                    return _serviceResultTemplate.BadRequest<string>("You've already reviewed this movie");
                }
                
                // Create a new user review entity
                var userReviewModel = new UserReviewModel()
                {
                    UserId = user.ID,
                    ReviewDate = DateTime.Now,
                    ReviewText = dto.ReviewText,
                    Rating = dto.Rating,
                    MovieId = dto.MovieId
                };

                // Save the user review entity to the database
                await _userReviewService.Add(userReviewModel);

                return _serviceResultTemplate.Success();
            }
            catch (Exception ex)
            {
                // _logger.LogError($/"Error posting review: {ex.Message}");
                return _serviceResultTemplate.InternalServerError<string>($"An error occurred while posting the review {ex.Message}");
            }
        }

    }
}