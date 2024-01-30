using Microsoft.EntityFrameworkCore;
using Service.Core.MySql;
using Movie.Common.Models.Movie;
using Movie.Common.Models.Genre;
using Movie.Common.Models.User;
using Movie.Common.Models.UserReview;

namespace Movie.Common.Contexts
{
    public class DatabaseContext : BaseDbContext
    {
        public virtual DbSet<MovieModel> Movie { get; set; }
        public virtual DbSet<GenreModel> Genre { get; set; }
        public virtual DbSet<UserModel> User { get; set; }
        public virtual DbSet<UserReviewModel> UserReview { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MovieModelTypeConfiguration());

        }
    }
}