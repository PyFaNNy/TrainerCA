using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainer.Persistence.Extensions;

namespace Trainer.Persistence.Configurations.Users
{
    public class PatientConfiguration : IEntityTypeConfiguration<Domain.Entities.Patient.Patient>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Patient.Patient> builder)
        {
            builder.ToTable("Patients", "user");

            builder.Property(x => x.LastName)
                .HasMediumMaxLength()
                .IsRequired();

            builder.Property(x => x.About)
                .HasMediumMaxLength()
                .IsRequired(false);

            builder.Property(x => x.Hobbies)
                .HasMediumMaxLength()
                .IsRequired(false);
        }
    }
}
