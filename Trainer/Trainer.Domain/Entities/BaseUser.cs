namespace Trainer.Domain.Entities
{
    using Trainer.Domain.Interfaces;
    using Trainer.Enums;

    public class BaseUser : IBaseEntity, IRemovedAt
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

        public string Email
        {
            get;
            set;
        }

        public UserRole Role
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

        public StatusUser Status
        {
            get;
            set;
        }

        public DateTime? RemovedAt
        {
            get;
            set;
        }
    }
}
