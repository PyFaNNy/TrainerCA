using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainer.Domain.Entities;
using Trainer.Persistence.Extensions;

namespace Trainer.Persistence.Configurations.Users
{
    public class BaseUserConfiguration : IEntityTypeConfiguration<BaseUser>
    {
        public void Configure(EntityTypeBuilder<BaseUser> builder)
        {
            builder.MapTable("BaseUsers", "user");

            builder.Property(x => x.Email)
                .HasMediumMaxLength()
                .IsRequired(false);

            builder.Property(x => x.FirstName)
                .HasMediumMaxLength()
                .IsRequired(false);

            builder.Property(x => x.LastName)
                .HasMediumMaxLength()
                .IsRequired(false);

            builder.Property(x => x.PasswordHash)
                .IsRequired(false);
        }
    }
}
