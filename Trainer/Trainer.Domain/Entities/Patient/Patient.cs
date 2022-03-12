using Trainer.Enums;

namespace Trainer.Domain.Entities.Patient
{
    public class Patient : BaseUser
    {
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
