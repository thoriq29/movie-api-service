using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movie.Common.Models.UserReview
{
    public class UserReviewModelTypeConfiguration : IEntityTypeConfiguration<UserReviewModel>
    {
        public void Configure(EntityTypeBuilder<UserReviewModel> builder)
        {
            builder.HasIndex(model => new { model.UserId, model.MovieId }).IsUnique();
            builder.ToTable("tb_user_review");
        }
    }
}
