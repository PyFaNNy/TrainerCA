using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Trainer.Persistence.Configurations.Examiantion
{
    public class ExaminationConfiguration : IEntityTypeConfiguration<Domain.Entities.Examination.Examination>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Examination.Examination> builder)
        {
            builder.HasOne(x => x.Patient)
                .WithMany(x => x.Examinations)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
