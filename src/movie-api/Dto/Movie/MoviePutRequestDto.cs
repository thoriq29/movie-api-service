using System;
using FluentValidation;

namespace Movie.Api.Dto.Movie
{
    public class MoviePutRequestDto: BaseMovieRequestDto
    {
        public long Id { get; set; }
    }

     public class MoviePutRequestDtoValidator : AbstractValidator<MoviePutRequestDto>
    {
        public MoviePutRequestDtoValidator()
        {
            // Validate input
            RuleFor(dto => dto.Id).GreaterThan(0).WithMessage("Id is required.");
            RuleFor(dto => dto.Title).NotNull().NotEmpty().WithMessage("Title is required.");
            RuleFor(dto => dto.ReleaseDate).NotNull().NotEmpty().WithMessage("Release date is required.");
            RuleFor(dto => dto.GenreId).NotNull().GreaterThan(0).NotEmpty().WithMessage("Genre ID is required.");
            RuleFor(dto => dto.Director).NotNull().NotEmpty().WithMessage("Director is required.");
            RuleFor(dto => dto.PlotSummary).NotNull().NotEmpty().WithMessage("Plot summary is required.");
            RuleFor(dto => dto.PosterUrl).NotNull().NotEmpty().WithMessage("Poster URL is required.");
        }
    }
}
