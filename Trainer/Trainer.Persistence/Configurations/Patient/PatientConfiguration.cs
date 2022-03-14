using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainer.Persistence.Extensions;

namespace Trainer.Persistence.Configurations.Patient
{
    public class PatientConfiguration : IEntityTypeConfiguration<Domain.Entities.Patient.Patient>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Patient.Patient> builder)
        {
            builder.ToTable("Patients", "patient");

            builder.Property(x => x.Email)
                .HasMediumMaxLength()
                .IsRequired();

            builder.Property(x => x.FirstName)
                .HasMediumMaxLength()
                .IsRequired();

            builder.Property(x => x.MiddleName)
                .HasMediumMaxLength()
                .IsRequired();

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
