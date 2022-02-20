using Trainer.Enums;

namespace Trainer.Domain.Entities.Patient
{
    public class Patient
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

        public int Age
        {
            get;
            set;
        }

        public Sex Sex
        {
            get;
            set;
        }

        public IList<Examination.Examination> Examinations
        {
            get;
            set;
        }

        public IList<Result.Result> Results
        {
            get;
            set;
        }

        public string About
        {
            get;
            set;
        }

        public string Hobbies
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }
    }
}
