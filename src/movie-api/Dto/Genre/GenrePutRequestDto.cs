using FluentValidation;

namespace Movie.Api.Dto.Genre
{
    public class GenrePutRequestDto:BaseGenreRequestDto
    {
        public long Id { get; set; }
    }

    public class GenrePutRequestDtoValidator : AbstractValidator<GenrePutRequestDto>
    {
        public GenrePutRequestDtoValidator()
        {
            //validate input
            RuleFor(dto => dto.Id).GreaterThan(0);
            RuleFor(dto => dto.Name).NotEmpty().NotNull().MaximumLength(30);
            RuleFor(dto => dto.Description).MaximumLength(100);
        }
    }
}
