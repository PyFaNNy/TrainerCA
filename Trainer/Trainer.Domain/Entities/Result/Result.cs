using Trainer.Enums;

namespace Trainer.Domain.Entities.Result
{
    public class Result
    {
        public Guid Id
        {
            get;
            set;
        }

        public Guid? PatientId
        {
            get;
            set;
        }

        public Guid? ExaminationId
        {
            get;
            set;
        }

        public int AverageHeartRate
        {
            get;
            set;
        }
        public int AverageDia
        {
            get;
            set;
        }

        public int AverageSis
        {
            get;
            set;
        }

        public int AverageOxigen
        {
            get;
            set;
        }
        public double AverageTemperature
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public TypePhysicalActive TypePhysicalActive
        {
            get;
            set;
        }

        public Patient.Patient Patient
        {
            get;
            set;
        }

        public Examination.Examination Examination
        {
            get;
            set;
        }
    }
}
