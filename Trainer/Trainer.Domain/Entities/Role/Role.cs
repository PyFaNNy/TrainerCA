using Microsoft.AspNetCore.Identity;

namespace Trainer.Domain.Entities.Role
{
    public class Role : IdentityRole<Guid>
    {
        public Role()
        {

        }
        public Role(string roleName)
        {
            Name = roleName;
            NormalizedName = roleName.ToUpper();
        }
    }
}
