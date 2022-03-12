namespace Trainer.Domain.Entities
{
    using Prixy.Enums;
    using Trainer.Domain.Interfaces;
    using Trainer.Enums;

    public class BaseUser : IBaseEntity
    {
        public Guid Id
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public UserRole UserRole
        {
            get;
            set;
        }

        public bool EmailConfirmed
        {
            get;
            set;
        }

        public string PasswordHash
        {
            get;
            set;
        }

        public RegistrationStatus Status
        {
            get;
            set;
        }
    }
}
