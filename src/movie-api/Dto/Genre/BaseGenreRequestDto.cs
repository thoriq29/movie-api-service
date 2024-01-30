using FluentValidation;

namespace Movie.Api.Dto.Genre
{
    public class BaseGenreRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
