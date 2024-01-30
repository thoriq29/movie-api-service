using System.Collections.Generic;
using AutoMapper;
using Movie.Common.Models.Movie;

namespace Movie.Api.Dto.Movie
{

    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            CreateMap<MoviePostRequestDto, MovieModel>();
            CreateMap<MoviePutRequestDto, MovieModel>();
            CreateMap<MovieModel, MovieDto>();
        }
    }
}