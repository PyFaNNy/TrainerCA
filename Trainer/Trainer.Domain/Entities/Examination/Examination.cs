using Trainer.Enums;

namespace Trainer.Domain.Entities.Examination
{
    public class Examination
    {
        public Guid Id
        {
            get;
            set;
        }

        public Guid PatientId
        {
            get;
            set;
        }

        public TypePhysicalActive TypePhysicalActive
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public int Indicators
        {
            get;
            set;
        }

        public ExaminationStatus Status
        {
            get;
            set;
        }

        public Patient.Patient Patient
        {
            get;
            set;
        }

        public Result.Result Result
        {
            get;
            set;
        }
    }
}
