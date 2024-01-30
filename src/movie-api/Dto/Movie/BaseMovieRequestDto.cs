using System;
using FluentValidation;

namespace Movie.Api.Dto.Movie
{
    public class BaseMovieRequestDto
    {
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public long GenreId { get; set; }

        public string Director { get; set; }

        public string PlotSummary { get; set; }

        public string PosterUrl { get; set; }
    }
}
