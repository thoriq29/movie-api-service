using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Movie.Api.Dto.Genre;
using Movie.Api.Dto.Movie;
using Movie.Api.Services.Caching;
using Movie.Api.Services.Query;
using Movie.Common.Services.Genre;
using Movie.Common.Services.Movie;
using Service.Core.Interfaces.Framework;
using Service.Core.Interfaces.Log;

namespace Movie.Api.Services.Query.Genre
{
    public class GenreQueryService : IGenreQueryService
    {
        private readonly ICoreLogger _logger;
        private readonly IErrorFactory _errorFactory;
        private readonly IServiceResultTemplate _serviceResultTemplate;
        private readonly IGenreService _genreService;
        private readonly IMovieService _movieService;
        private readonly IMovieCachingService _movieCachingService;
        private readonly IMapper _mapper;

        public GenreQueryService(
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

        public async Task<IServiceApiResult<GenreDto>> GetGenreDetail(long genreId)
        {
            try
            {
                var genre = await _genreService.Find(genreId);
                if (genre == null)
                {
                    return _serviceResultTemplate.NotFound<GenreDto>("Genre not found");
                }

                var movies = await _movieService.FindMany(it => it.GenreId == genre.ID && it.DeletedDate == null);

                var genreDto = _mapper.Map<GenreDto>(genre);

                var moviesDto = _mapper.Map<List<MovieDto>>(movies);

                genre.Movies = movies;
                genre.ID = genre.ID;
                return _serviceResultTemplate.SuccessWithData(genreDto);
            }
            catch (Exception ex)
            {
                return _serviceResultTemplate.InternalServerError<GenreDto>($"An error occurred while getting genre detail {ex.Message}");
            }
        }

        public async Task<IServiceApiResult<List<GenreDto>>> GetListGenre()
        {
            try
            {
                var cacheData = await _movieCachingService.GetListGenreDto();
                if (cacheData != null)
                {
                    return _serviceResultTemplate.SuccessWithData(cacheData);
                }
                var genres = await _genreService.FindMany(it => it.DeletedDate == null);
                var genreDtos = new List<GenreDto>();

                foreach (var genre in genres)
                {
                    var genreDto = _mapper.Map<GenreDto>(genre);

                    // Fetch associated movies for the genre
                    var movies = await _movieService.FindMany(it => it.GenreId == genre.ID && it.DeletedDate == null);

                    // Map movies to MovieDto objects
                    var movieDtos = _mapper.Map<List<MovieDto>>(movies);

                    // Assign movieDtos to genreDto's Movies property
                    genreDto.Movies = movieDtos;

                    genreDtos.Add(genreDto);
                }
                await _movieCachingService.SetGenreListToCache(genreDtos);
                return _serviceResultTemplate.SuccessWithData(genreDtos);
            }
            catch (System.Exception ex)
            {
                return _serviceResultTemplate.InternalServerError<List<GenreDto>>($"An error occurred while getting list of genres {ex.Message}");
            }
        }
    }
}
