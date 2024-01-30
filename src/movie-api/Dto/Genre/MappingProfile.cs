using AutoMapper;
using Movie.Api.Dto.Genre;
using Movie.Common.Models.Genre;

namespace Movie.Api.Dto.Movie
{

    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile()
        {
            CreateMap<GenrePostRequestDto, GenreModel>();
            CreateMap<GenrePutRequestDto, GenreModel>();
            CreateMap<GenreModel, GenreDto>();
            // CreateMap<TemplateModel, TemplateDto>();
        }
    }
}