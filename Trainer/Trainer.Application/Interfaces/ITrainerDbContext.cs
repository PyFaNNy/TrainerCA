using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainer.Domain.Entities.Examination;
using Trainer.Domain.Entities.Patient;
using Trainer.Domain.Entities.Result;
using Trainer.Domain.Entities.Role;
using Trainer.Domain.Entities.User;

namespace Trainer.Application.Interfaces
{
    public interface ITrainerDbContext
    {
        DbSet<Patient> Patients
        {
            get;
            set;
        }

        DbSet<Examination> Examinations
        {
            get;
            set;
        }

        DbSet<Result> Results
        {
            get;
            set;
        }

        DbSet<User> Users
        {
            get;
            set;
        }

        DbSet<Role> Roles
        {
            get;
            set;
        }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        int SaveChanges();
    }
}
