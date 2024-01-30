using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Movie.Api.Dto.Movie;
using Movie.Api.Services.Caching;
using Movie.Common.Services.Genre;
using Movie.Common.Services.Movie;
using Service.Core.Interfaces.Framework;
using Service.Core.Interfaces.Log;

namespace Movie.Api.Services.Query.Movie
{
    public class MovieQueryService : IMovieQueryService
    {
        private readonly ICoreLogger _logger;
        private readonly IErrorFactory _errorFactory;
        private readonly IServiceResultTemplate _serviceResultTemplate;
        private readonly IGenreService _genreService;
        private readonly IMovieService _movieService;
        private readonly IMovieCachingService _movieCachingService;
        private readonly IMapper _mapper;

        public MovieQueryService(
            ICoreLogger logger,
            IErrorFactory errorFactory,
            IServiceResultTemplate serviceResultTemplate,
            IGenreService genreService,
            IMovieService movieService,
            IMovieCachingService movieCachingService,
            IMapper mapper
        )
        {
            _logger = logger;
            _errorFactory = errorFactory;
            _serviceResultTemplate = serviceResultTemplate;
            _genreService = genreService;
            _movieService = movieService;
            _movieCachingService = movieCachingService;
            _mapper = mapper;
        }

        public async Task<IServiceApiResult<MovieDto>> GetMovieDetail(long movieId)
        {
            try
            {
                var movie = await _movieService.Find(movieId);
                if (movie == null)
                {
                    return _serviceResultTemplate.NotFound<MovieDto>("Movie not found");
                }

                var genre = await _genreService.Find(it => it.ID == movie.GenreId && it.DeletedDate == null);

                var movieDto = _mapper.Map<MovieDto>(movie);
                movieDto.GenreName = genre.Name;

                return _serviceResultTemplate.SuccessWithData(movieDto);
            }
            catch (Exception ex)
            {
                return _serviceResultTemplate.InternalServerError<MovieDto>($"An error occurred while getting movie detail {ex.Message}");
            }
        }

        public async Task<IServiceApiResult<List<MovieDto>>> GetListMovie()
        {
            try
            {
                var cacheData = await _movieCachingService.GetListMovieDto();
                if (cacheData != null)
                {
                    return _serviceResultTemplate.SuccessWithData(cacheData);
                }
                var movies = await _movieService.FindMany(it => it.DeletedDate == null);
                var movieDtos = new List<MovieDto>();
                foreach (var movie in movies)
                {
                    var genre = await _genreService.Find(it => it.ID == movie.GenreId && it.DeletedDate == null);

                    var movieDto = _mapper.Map<MovieDto>(movie);
                    movieDto.GenreName = genre?.Name; // Assign genre name to MovieDto

                    movieDtos.Add(movieDto);
                }
                await _movieCachingService.SetMovieListToCache(movieDtos);
                return _serviceResultTemplate.SuccessWithData(movieDtos);
            }
            catch (System.Exception ex)
            {
                return _serviceResultTemplate.InternalServerError<List<MovieDto>>($"An error occurred while getting list of movies {ex.Message}");
            }
        }
    }
}
