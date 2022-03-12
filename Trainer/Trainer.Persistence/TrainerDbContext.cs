using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Trainer.Application.Interfaces;
using Trainer.Domain.Entities;
using Trainer.Domain.Entities.Admin;
using Trainer.Domain.Entities.Doctor;
using Trainer.Domain.Entities.Examination;
using Trainer.Domain.Entities.Manager;
using Trainer.Domain.Entities.Patient;
using Trainer.Domain.Entities.Result;
using Trainer.Domain.Interfaces;

namespace Trainer.Persistence
{
    public class TrainerDbContext : DbContext, ITrainerDbContext
    {
        public DbSet<Patient> Patients 
        {
            get;
            set;
        }

        public DbSet<Examination> Examinations 
        { 
            get;
            set;
        }

        public DbSet<Result> Results 
        { 
            get;
            set;
        }

        public DbSet<BaseUser> BaseUsers
        {
            get;
            set;
        }

        public DbSet<Admin> Admins
        {
            get;
            set;
        }

        public DbSet<Doctor> Doctors
        {
            get;
            set;
        }

        public DbSet<Manager> Managers
        {
            get;
            set;
        }

        public DbSet<OTP> OTPs
        {
            get;
            set;
        }

        public TrainerDbContext(DbContextOptions<TrainerDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            this.SetCreatedAt();
            this.SetUpdatedAt();
            var result = base.SaveChanges();

            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            this.SetCreatedAt();
            this.SetUpdatedAt();

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        private void SetCreatedAt()
        {
            foreach (var entry in this.ChangeTracker.Entries()
                         .Where(p => p.State == EntityState.Added))
            {
                if (entry.Entity is ICreatedAt ent)
                {
                    ent.CreatedAt = DateTime.UtcNow;
                }
            }
        }

        private void SetUpdatedAt()
        {
            foreach (var entry in this.ChangeTracker.Entries()
                         .Where(p => p.State == EntityState.Modified))
            {
                if (entry.Entity is IUpdatedAt ent)
                {
                    ent.UpdatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
