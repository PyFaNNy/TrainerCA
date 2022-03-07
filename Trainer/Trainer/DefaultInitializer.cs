namespace Trainer
{
    using Microsoft.AspNetCore.Identity;
    using Trainer.Domain.Entities.Role;
    using Trainer.Domain.Entities.User;

    public class DefaultInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new Role("admin"));
            }

            if (await roleManager.FindByNameAsync("doctor") == null)
            {
                await roleManager.CreateAsync(new Role("doctor"));
            }

            if (await roleManager.FindByNameAsync("manager") == null)
            {
                await roleManager.CreateAsync(new Role("manager"));
            }

            if (await roleManager.FindByNameAsync("unknown") == null)
            {
                await roleManager.CreateAsync(new Role("unknown"));
            }

            if (await userManager.FindByEmailAsync("admin@gmail.com") == null)
            {
                User admin = new User { Email = "admin@gmail.com", UserName = "admin" };
                IdentityResult result = await userManager.CreateAsync(admin, "admin");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }

            if (await userManager.FindByEmailAsync("doctor@gmail.com") == null)
            {
                User doctor = new User { Email = "doctor@gmail.com", UserName = "doctor" };
                IdentityResult result = await userManager.CreateAsync(doctor, "doctor");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(doctor, "doctor");
                }
            }

            if (await userManager.FindByEmailAsync("manager@gmail.com") == null)
            {
                User manager = new User { Email = "manager@gmail.com", UserName = "manager" };
                IdentityResult result = await userManager.CreateAsync(manager, "doctor");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(manager, "manager");
                }
            }
        }
    }
}
