using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Trainer.Persistence.Configurations.Results
{
    public class ResultsConfiguration : IEntityTypeConfiguration<Domain.Entities.Result.Result>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Result.Result> builder)
        {
            builder.HasOne(x => x.Examination)
                .WithOne(x => x.Result)
                .HasForeignKey<Domain.Entities.Result.Result>(x => x.ExaminationId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.Results)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
