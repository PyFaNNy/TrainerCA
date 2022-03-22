namespace Trainer
{
    using Trainer.Application.Interfaces;
    using Trainer.Common.TableConnect.Common;
    using Trainer.Domain.Entities.Admin;
    using Trainer.Domain.Entities.Doctor;
    using Trainer.Domain.Entities.Manager;

    public class DefaultInitializer
    {
        public static async Task InitializeAsync(ITrainerDbContext dbContext)
        {
            if (!dbContext.Admins.Any())
            {
                dbContext.Admins.Add(new Admin
                {
                    Id = Guid.Parse("19f7d733-8826-4726-a669-0d29a882eda4"),
                    //Email = "traineradmin@gmail.com",
                    Email = "fanny.koob@gmail.com",
                    PasswordHash = CryptoHelper.HashPassword("admin"),
                    FirstName = "Admin",
                    MiddleName = "Admin",
                    LastName = "Admin",
                    Role = Enums.UserRole.Admin,
                });
                dbContext.SaveChanges();
            }

            if (!dbContext.Doctors.Any())
            {
                dbContext.Doctors.Add(new Doctor
                {
                    Id = Guid.Parse("19f7d733-8826-4726-a669-0d29a8821da4"),
                    Email = "trainerdoctor@gmail.com",
                    PasswordHash = CryptoHelper.HashPassword("doctor"),
                    FirstName = "Doctor",
                    MiddleName = "Doctor",
                    LastName = "Doctor",
                    Role = Enums.UserRole.Doctor,
                });
                dbContext.SaveChanges();
            }

            if (!dbContext.Managers.Any())
            {
                dbContext.Managers.Add(new Manager
                {
                    Id = Guid.Parse("19f7d733-8826-4726-a669-1d29a882eda4"),
                    Email = "trainermanager@gmail.com",
                    PasswordHash = CryptoHelper.HashPassword("manager"),
                    FirstName = "Manager",
                    MiddleName = "Manager",
                    LastName = "Manager",
                    Role = Enums.UserRole.Manager,
                });
                dbContext.SaveChanges();
            }
        }
    }
}
