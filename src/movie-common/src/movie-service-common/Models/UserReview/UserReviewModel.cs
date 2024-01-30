using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Movie.Common.Models.Movie;
using Movie.Common.Models.User;
using Service.Core.MySql;

namespace Movie.Common.Models.UserReview
{
    public class UserReviewModel: BaseModel
    {
        [Column("user_id")]
        public long UserId { get; set; }

        [Required]
        [Column("movie_id")]
        public long MovieId { get; set; }

        [Required]
        [Column("review_text")]
        public string ReviewText { get; set; }

        [Required]
        [Column("rating")]
        public double Rating { get; set; }

        [Required]
        [Column("review_date")]
        public DateTime ReviewDate { get; set; }

        [Column("deleted_date")]
        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted()
        {
            return DeletedDate.HasValue;
        }


        public MovieModel Movie {get; set;}
        public UserModel User {get; set;}
    }
}
