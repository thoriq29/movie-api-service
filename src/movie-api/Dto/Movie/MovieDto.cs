using System;

namespace Movie.Api.Dto.Movie
{
    public class MovieDto
    {
        public long ID { get; set; }
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public long GenreId { get; set; }
        public string GenreName { get; set; }

        public string Director { get; set; }

        public string PlotSummary { get; set; }

        public string PosterUrl { get; set; }
    }
}
