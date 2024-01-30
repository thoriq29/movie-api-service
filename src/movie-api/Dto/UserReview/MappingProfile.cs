using AutoMapper;
using Movie.Common.Models.UserReview;

namespace Movie.Api.Dto.UserReview
{

    public class UserReviewMappingProfile : Profile
    {
        public UserReviewMappingProfile()
        {
            CreateMap<UserReviewRequestDto, UserReviewModel>();
            CreateMap<UserReviewModel, UserReviewDto>();
            // CreateMap<TemplateModel, TemplateDto>();
        }
    }
}