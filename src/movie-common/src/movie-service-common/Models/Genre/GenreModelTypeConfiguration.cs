using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movie.Common.Models.Genre
{
    public class GenreModelTypeConfiguration : IEntityTypeConfiguration<GenreModel>
    {
        public void Configure(EntityTypeBuilder<GenreModel> builder)
        {
            builder.HasIndex(model => model.Name).IsUnique();
            builder.HasMany(movie => movie.Movies)
               .WithOne(genre => genre.Genre)
               .HasForeignKey(genre => genre.GenreId);

            builder.ToTable("tb_genre");
        }
    }
}
