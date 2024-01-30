using FluentValidation;

namespace Movie.Api.Dto.UserReview
{
    public class UserReviewGetRequestDto
    {
        public string AccountId { get; set; }
        public long MovieId { get; set; }
    }
}
