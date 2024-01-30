using System;
using FluentValidation;
using Movie.Api.Dto.Movie;

namespace Movie.Api.Dto.UserReview
{
    public class UserReviewDto
    {
        public string UserName { get; set; }
        public long MovieId { get; set; }
        public MovieDto Movie { get; set; }
        public double Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }
        
    }
}
