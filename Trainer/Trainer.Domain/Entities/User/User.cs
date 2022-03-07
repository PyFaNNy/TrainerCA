using Microsoft.AspNetCore.Identity;
using Trainer.Enums;

namespace Trainer.Domain.Entities.User
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName 
        {
            get;
            set;
        }

        public string MiddleName
        {
            get;
            set;
        }

        public string LastName 
        { 
            get;
            set;
        }

        public StatusUser Status
        {
            get;
            set;
        }

        public bool RememberMe 
        { 
            get;
            set;
        }
    }
}
