namespace Trainer.Settings.Error
{
    public class PatientErrorSettings : IApplicationSettings
    {
        public bool CreatePatientEnable
        {
            get;
            set;
        }

        public bool DeletePatientEnable
        {
            get;
            set;
        }

        public bool UpdatePatientEnable
        {
            get;
            set;
        }

        public bool GetPatientEnable
        {
            get;
            set;
        }

        public bool GetRandomPatientEnable
        {
            get;
            set;
        }

        public bool GetPatientsEnable
        {
            get;
            set;
        }

        public bool GetRandomPatientsEnable
        {
            get;
            set;
        }
    }
}
