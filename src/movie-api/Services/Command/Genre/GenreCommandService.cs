
using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Movie.Api.Dto.Genre;
using Movie.Api.Services.Caching;
using Movie.Api.Services.Command.Genre;
using Movie.Api.Utils;
using Movie.Common.Models.Genre;
using Movie.Common.Services.Genre;
using Service.Core.Interfaces.Framework;
using Service.Core.Interfaces.Log;

namespace Movie.Api.Services.Command.Genre
{
    public class GenreCommandService : IGenreCommandService
    {
        private readonly ICoreLogger _logger;
        private readonly IErrorFactory _errorFactory;
        private readonly IServiceResultTemplate _serviceResultTemplate;
        private readonly IValidator<GenrePostRequestDto> _genrePostRequestDtoValidator;
        private readonly IValidator<GenrePutRequestDto> _genrePutRequestValidator;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        private readonly IMovieCachingService _movieCachingService;

        public GenreCommandService(
            ICoreLogger logger,
            IErrorFactory errorFactory,
            IServiceResultTemplate serviceResultTemplate,
            IValidator<GenrePostRequestDto> genrePostRequestDtoValidator,
            IValidator<GenrePutRequestDto> genrePutRequestDtoValidator,
            IGenreService genreService,
            IMapper mapper,
            IMovieCachingService movieCachingService
        )
        {
            _logger = logger;
            _errorFactory = errorFactory;
            _serviceResultTemplate = serviceResultTemplate;
            _genrePostRequestDtoValidator = genrePostRequestDtoValidator;
            _genrePutRequestValidator = genrePutRequestDtoValidator;
            _genreService = genreService;
            _mapper = mapper;
            _movieCachingService = movieCachingService;
        }

        public async Task<IServiceApiResult<string>> CreateGenre(GenrePostRequestDto dto)
        {
            
            try
            {
                // validate request body
                var requestDataValidationResult = await _genrePostRequestDtoValidator.ValidateAsync(dto);
                if (!requestDataValidationResult.IsValid)
                {
                    var error = ErrorUtils.E123001(requestDataValidationResult.ToString("~"), nameof(CreateGenre));
                    _logger.LogInformation(error.message);
                    return _serviceResultTemplate.BadRequest<string>(error.message);
                }

                // check if genre already exist or not
                var previousGenre = await _genreService.Find(it => it.Name == dto.Name);
                if(previousGenre != null && !previousGenre.IsDeleted())
                {
                    // if exist, return bad request
                    var error = ErrorUtils.E123002(dto.Name, nameof(CreateGenre));
                    _logger.LogInformation(error.message);
                    return _serviceResultTemplate.BadRequest<string>(error.message);
                }

                // save genre data
                var genreModel = _mapper.Map<GenrePostRequestDto, GenreModel>(dto);
                await _genreService.Add(genreModel);

                await _movieCachingService.DeleteCache();
                return _serviceResultTemplate.Success<string>();

            }
            catch (Exception e)
            {
                _logger.LogError(ErrorUtils.E123004, e.Message);   
                return _serviceResultTemplate.InternalServerError<string>(e.Message);
            }
        }

        public async Task<IServiceApiResult<string>> UpdateGenre(GenrePutRequestDto dto)
        {
            try
            {
                // validate request body
                var requestDataValidationResult = await _genrePutRequestValidator.ValidateAsync(dto);
                if (!requestDataValidationResult.IsValid)
                {
                    var error = ErrorUtils.E123001(requestDataValidationResult.ToString("~"), nameof(CreateGenre));
                    _logger.LogInformation(error.message);
                    return _serviceResultTemplate.BadRequest<string>(ErrorUtils.E123001(requestDataValidationResult.ToString("~"), nameof(CreateGenre)).message);
                }

                // check if genre already exist or not
                var genreData = await _genreService.Find(dto.Id);
                if(genreData == null || genreData.IsDeleted())
                {
                    // if not exist, return bad request genre not found
                    var error = ErrorUtils.E123002(dto.Name, nameof(CreateGenre));
                    _logger.LogInformation(error.message);
                    return _serviceResultTemplate.BadRequest<string>(ErrorUtils.E123003(dto.Id.ToString(), nameof(UpdateGenre)).message);
                }

                // save genre data
                if (!dto.Description.Equals(genreData.Description))
                {
                    genreData.Description = dto.Description;
                }

                if (!dto.Name.Equals(genreData.Name))
                {
                    genreData.Name = dto.Name;
                }

                await _genreService.Update(genreData);
                await _movieCachingService.DeleteCache();
                return _serviceResultTemplate.Success<string>();

            }
            catch (Exception e)
            {
                _logger.LogError(ErrorUtils.E123004, e);
                return _serviceResultTemplate.InternalServerError<string>(e.Message);
            }
        }
    }
}