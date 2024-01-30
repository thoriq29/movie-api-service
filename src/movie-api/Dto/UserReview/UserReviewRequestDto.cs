using FluentValidation;

namespace Movie.Api.Dto.UserReview
{
    public class UserReviewRequestDto
    {
        public string AccountId { get; set; }
        public long MovieId { get; set; }
        public double Rating { get; set; }
        public string ReviewText { get; set; }
    }

    public class UserReviewRequestDtoValidator : AbstractValidator<UserReviewRequestDto>
    {
        public UserReviewRequestDtoValidator()
        {
            // Validate input
            RuleFor(dto => dto.MovieId).NotNull().NotEmpty().GreaterThan(0).WithMessage("Movie ID is required.");
            RuleFor(dto => dto.Rating).InclusiveBetween(0, 5).WithMessage("Rating must be between 0 and 5.");
            RuleFor(dto => dto.ReviewText).NotNull().NotEmpty().WithMessage("Review text is required.");
        }
    }
}
