
using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Movie.Api.Dto.Movie;
using Movie.Api.Services.Caching;
using Movie.Api.Services.Command.Movie;
using Movie.Api.Utils;
using Movie.Common.Models.Movie;
using Movie.Common.Services.Genre;
using Movie.Common.Services.Movie;
using Service.Core.Interfaces.Framework;
using Service.Core.Interfaces.Log;

namespace Movie.Api.Services.Command.Movie
{
    public class MovieCommandService : IMovieCommandService
    {
        private readonly ICoreLogger _logger;
        private readonly IErrorFactory _errorFactory;
        private readonly IServiceResultTemplate _serviceResultTemplate;
        private readonly IValidator<MoviePostRequestDto> _moviePostRequestDtoValidator;
        private readonly IValidator<MoviePutRequestDto> _moviePutRequestDtoValidator;
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IMovieCachingService _movieCachingService;
        private readonly IMapper _mapper;

        public MovieCommandService(
            ICoreLogger logger,
            IErrorFactory errorFactory,
            IServiceResultTemplate serviceResultTemplate,
            IValidator<MoviePostRequestDto> moviePostRequestDtoValidator,
            IValidator<MoviePutRequestDto> moviePutRequestDtoValidator,
            IMovieService movieService,
            IGenreService genreService,
            IMapper mapper,
            IMovieCachingService movieCachingService
        )
        {
            _logger = logger;
            _errorFactory = errorFactory;
            _serviceResultTemplate = serviceResultTemplate;
            _moviePostRequestDtoValidator = moviePostRequestDtoValidator;
            _moviePutRequestDtoValidator = moviePutRequestDtoValidator;
            _movieService = movieService;
            _genreService = genreService;
            _mapper = mapper;
            _movieCachingService = movieCachingService;
        }

        public async Task<IServiceApiResult<string>> CreateMovie(MoviePostRequestDto dto)
        {
            try
            {
                // Validate input DTO
                var requestDataValidationResult = await _moviePostRequestDtoValidator.ValidateAsync(dto);
                if (!requestDataValidationResult.IsValid)
                {
                    var error = ErrorUtils.E123001(requestDataValidationResult.ToString("~"), nameof(CreateMovie));
                    _logger.LogInformation(error.message);
                    return _serviceResultTemplate.BadRequest<string>(error.message);
                }
                var previousMovie = await _movieService.Find(it => it.Title == dto.Title);
                if (previousMovie != null && !previousMovie.IsDeleted())
                {
                    return _serviceResultTemplate.BadRequest<string>("Movie with this title is already exists");
                }

                var genreData = await _genreService.Find(dto.GenreId);
                if(genreData == null || genreData.IsDeleted())
                {
                    // if not exist, return bad request genre not found
                    var error = ErrorUtils.E123002(dto.GenreId.ToString(), nameof(CreateMovie));
                    _logger.LogInformation(error.message);
                    return _serviceResultTemplate.BadRequest<string>(ErrorUtils.E123003(dto.GenreId.ToString(), nameof(CreateMovie)).message);
                }

                // Map DTO to movie model and create the movie
                var movieModel = _mapper.Map<MoviePostRequestDto, MovieModel>(dto);
                await _movieService.Add(movieModel);
                await _movieCachingService.DeleteCache();
                return _serviceResultTemplate.Success();
            }
            catch(Exception e)
            {
                _logger.LogError(ErrorUtils.E123004, e.Message);   
                return _serviceResultTemplate.InternalServerError<string>(e.Message);
            }
        }

        public async Task<IServiceApiResult<string>> DeleteMovie(long movieId)
        {
            try
            {
                if (movieId <= 0)
                {
                    var error = ErrorUtils.E123001("Invalid movie id", nameof(DeleteMovie));
                    _logger.LogInformation(error.message);
                    return _serviceResultTemplate.BadRequest<string>(error.message);
                }

                var movieData = await _movieService.Find(movieId);
                if (movieData == null || movieData.IsDeleted())
                {
                    return _serviceResultTemplate.BadRequest<string>("Movie with this title is not found");
                }

                movieData.DeletedDate = DateTime.Now;
                await _movieService.Update(movieData);
                await _movieCachingService.DeleteCache();
               return _serviceResultTemplate.Success();
            }
            catch(Exception e)
            {
                _logger.LogError(ErrorUtils.E123004, e.Message);   
                return _serviceResultTemplate.InternalServerError<string>(e.Message);
            } 
        }

        public async Task<IServiceApiResult<string>> UpdateMovie(MoviePutRequestDto dto)
        {
            try 
            {
                // Validate input DTO
                var requestDataValidationResult = await _moviePutRequestDtoValidator.ValidateAsync(dto);
                if (!requestDataValidationResult.IsValid)
                {
                    var error = ErrorUtils.E123001(requestDataValidationResult.ToString("~"), nameof(CreateMovie));
                    _logger.LogInformation(error.message);
                    return _serviceResultTemplate.BadRequest<string>(error.message);
                }

                var movieData = await _movieService.Find(dto.Id);
                if (movieData == null || movieData.IsDeleted())
                {
                    return _serviceResultTemplate.BadRequest<string>("Movie with this title is not found");
                }

                var genreData = await _genreService.Find(dto.GenreId);
                if(genreData == null || genreData.IsDeleted())
                {
                    // if not exist, return bad request genre not found
                    var error = ErrorUtils.E123002(dto.GenreId.ToString(), nameof(CreateMovie));
                    _logger.LogInformation(error.message);
                    return _serviceResultTemplate.BadRequest<string>(ErrorUtils.E123003(dto.GenreId.ToString(), nameof(CreateMovie)).message);
                }

                // Map DTO to movie model and update the movie
                // Update the movie entity with the new data
                if (movieData.Title != dto.Title)
                {
                    movieData.Title = dto.Title;
                }

                if (movieData.ReleaseDate != dto.ReleaseDate)
                {
                    movieData.ReleaseDate = dto.ReleaseDate;
                }

                if (movieData.GenreId != dto.GenreId)
                {
                    movieData.GenreId = dto.GenreId;
                }

                if (movieData.Director != dto.Director)
                {
                    movieData.Director = dto.Director;
                }

                if (movieData.PlotSummary != dto.PlotSummary)
                {
                    movieData.PlotSummary = dto.PlotSummary;
                }

                if (movieData.PosterUrl != dto.PosterUrl)
                {
                    movieData.PosterUrl = dto.PosterUrl;
                }

                await _movieService.Update(movieData);
                await _movieCachingService.DeleteCache();
                return _serviceResultTemplate.Success();
            }
            catch(Exception e)
            {
                _logger.LogError(ErrorUtils.E123004, e.Message);   
                return _serviceResultTemplate.InternalServerError<string>(e.Message);
            } 
        }
    }
}