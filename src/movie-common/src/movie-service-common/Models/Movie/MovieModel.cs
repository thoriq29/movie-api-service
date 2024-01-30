using Movie.Common.Models.Genre;
using Movie.Common.Models.UserReview;
using Service.Core.MySql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Common.Models.Movie
{
    public class MovieModel : BaseModel
    {
        [Required]
        [Column("title")]
        public string Title { get; set; }
        [Required]
        [Column("release_date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Column("genre_id")]
        public long GenreId { get; set; }

        [Required]
        [Column("director")]
        public string Director { get; set; }

        [Required]
        [Column("plot_summary")]
        public string PlotSummary { get; set; }

        [Required]
        [Column("poster_url")]
        public string PosterUrl { get; set; }

        [Column("deleted_date")]
        public DateTime? DeletedDate { get; set; }

        public GenreModel Genre { get; set; }
        public ICollection<UserReviewModel> Reviews { get; set; }

        public bool IsDeleted()
        {
            return DeletedDate.HasValue;
        }
    }
}
