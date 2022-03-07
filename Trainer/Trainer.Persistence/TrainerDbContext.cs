﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Trainer.Application.Interfaces;
using Trainer.Domain.Entities.Examination;
using Trainer.Domain.Entities.Patient;
using Trainer.Domain.Entities.Result;
using Trainer.Domain.Entities.Role;
using Trainer.Domain.Entities.User;
using Trainer.Domain.Interfaces;

namespace Trainer.Persistence
{
    public class TrainerDbContext : IdentityDbContext<User, Role, Guid>, ITrainerDbContext
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

        public DbSet<User> Users
        {
            get;
            set;
        }

        public DbSet<Role> Roles
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
