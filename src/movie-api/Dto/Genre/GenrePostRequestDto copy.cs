using FluentValidation;

namespace Movie.Api.Dto.Genre
{
    public class GenrePostRequestDto:BaseGenreRequestDto
    {
    }

    public class GenrePostRequestDtoValidator : AbstractValidator<GenrePostRequestDto>
    {
        public GenrePostRequestDtoValidator()
        {
            //validate input
            RuleFor(dto => dto.Name).NotEmpty().NotNull().MaximumLength(30);
            RuleFor(dto => dto.Description).MaximumLength(100);
        }
    }
}
