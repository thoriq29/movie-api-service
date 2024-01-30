using System;
using FluentValidation;

namespace Movie.Api.Dto.Movie
{
    public class MoviePostRequestDto: BaseMovieRequestDto
    {
        
    }

     public class MoviePostRequestDtoValidator : AbstractValidator<MoviePostRequestDto>
    {
        public MoviePostRequestDtoValidator()
        {
            // Validate input
            RuleFor(dto => dto.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(dto => dto.ReleaseDate).NotEmpty().WithMessage("Release date is required.");
            RuleFor(dto => dto.GenreId).NotEmpty().GreaterThan(0).WithMessage("Genre ID is required.");
            RuleFor(dto => dto.Director).NotEmpty().WithMessage("Director is required.");
            RuleFor(dto => dto.PlotSummary).NotEmpty().WithMessage("Plot summary is required.");
            RuleFor(dto => dto.PosterUrl).NotEmpty().WithMessage("Poster URL is required.");
        }
    }
}
