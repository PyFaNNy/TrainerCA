using Microsoft.EntityFrameworkCore;
using Trainer.Domain.Entities;
using Trainer.Domain.Entities.Admin;
using Trainer.Domain.Entities.Doctor;
using Trainer.Domain.Entities.Examination;
using Trainer.Domain.Entities.Manager;
using Trainer.Domain.Entities.Patient;
using Trainer.Domain.Entities.Result;

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

        DbSet<BaseUser> BaseUsers
        {
            get;
            set;
        }

        DbSet<Admin> Admins
        {
            get;
            set;
        }

        DbSet<Doctor> Doctors
        {
            get;
            set;
        }

        DbSet<Manager> Managers
        {
            get;
            set;
        }

        DbSet<OTP> OTPs
        {
            get;
            set;
        }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        int SaveChanges();
    }
}
