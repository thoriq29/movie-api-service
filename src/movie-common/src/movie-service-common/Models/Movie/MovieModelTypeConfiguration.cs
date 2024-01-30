using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movie.Common.Models.Movie
{
    public class MovieModelTypeConfiguration : IEntityTypeConfiguration<MovieModel>
    {
        public void Configure(EntityTypeBuilder<MovieModel> builder)
        {
            builder.HasMany(movie => movie.Reviews)
               .WithOne(review => review.Movie)
               .HasForeignKey(review => review.MovieId);

            builder.ToTable("tb_movie");
        }
    }
}
