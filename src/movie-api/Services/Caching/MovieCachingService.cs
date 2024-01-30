using AutoMapper;
using Service.Core.Interfaces.Caching;
using Service.Core.Interfaces.Framework;
using Service.Core.Interfaces.Log;
using Service.Core.Log.Errors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie.Api.Dto.Genre;
using Movie.Api.Dto.Movie;

namespace Movie.Api.Services.Caching
{
    public sealed class MovieCachingService : IMovieCachingService
    {

        private readonly ICachingService _cachingService;
        private readonly ICoreLogger _logger;

        private const string genreListCacheName = "genres";
        private const string movieListCacheName = "movies";

        public MovieCachingService(
            ICoreLogger logger,
            ICachingService cachingService
            )
        {
            _cachingService = cachingService;
            _logger = logger;
        }

        public async Task<List<GenreDto>> GetListGenreDto()
        {
            try
            {
                var cacheData = await _cachingService.GetCache($"{genreListCacheName}", false);
                if (cacheData == null) return null;
                var response = JsonConvert.DeserializeObject<List<GenreDto>>(cacheData);
                return response;
            }
            catch(Exception e)
            {
                _logger.LogWarning(new Error()
                {
                    code = "-",
                    message = e.Message,
                }, "Error getting cache genre list");
                throw;
            }
        }

        public async Task<bool> SetGenreListToCache(List<GenreDto> data)
        {
            try
            {
                await _cachingService.DeleteCacheData(genreListCacheName, false);
                await _cachingService.SetCache($"{genreListCacheName}", data, 7*24*3600, false);
                return false;
            }
            catch(Exception e)
            {
                _logger.LogWarning(new Error()
                {
                    code = "-",
                    message = e.Message,
                }, "Error set cache genre list");
                throw;
            }
        }

        public async Task<List<MovieDto>> GetListMovieDto()
        {
            try
            {
                var cacheData = await _cachingService.GetCache($"{movieListCacheName}", false);
                if (cacheData == null) return null;
                var response = JsonConvert.DeserializeObject<List<MovieDto>>(cacheData);
                return response;
            }
            catch(Exception e)
            {
                _logger.LogWarning(new Error()
                {
                    code = "-",
                    message = e.Message,
                }, "Error getting cache movie list");
                throw;
            }
        }

        public async Task<bool> SetMovieListToCache(List<MovieDto> data)
        {
            try
            {
                await _cachingService.DeleteCacheData(movieListCacheName, false);
                await _cachingService.SetCache($"{movieListCacheName}", data, 7*24*3600, false);
                return false;
            }
            catch(Exception e)
            {
                _logger.LogWarning(new Error()
                {
                    code = "-",
                    message = e.Message,
                }, "Error set cache movie list");
                throw;
            }
        }

        public async Task<bool> DeleteCache()
        {
            
            try
            {
                await _cachingService.DeleteCacheData(movieListCacheName, false);
                await _cachingService.DeleteCacheData(genreListCacheName, false);
                return false;
            }
            catch(Exception e)
            {
                _logger.LogWarning(new Error()
                {
                    code = "-",
                    message = e.Message,
                }, "Error delete cache list");
                throw;
            }
        }
    }
}