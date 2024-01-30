using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movie.Common.Models.User
{
    public class UserModelTypeConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.HasIndex(model => model.AccountId).IsUnique();
            builder.HasMany(movie => movie.Reviews)
               .WithOne(review => review.User)
               .HasForeignKey(review => review.UserId);
       
            builder.ToTable("tb_user");
        }
    }
}
