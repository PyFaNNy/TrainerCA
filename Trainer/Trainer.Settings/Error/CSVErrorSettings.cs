namespace Trainer.Settings.Error
{
    public class CSVErrorSettings : IApplicationSettings
    {
        public bool CSVToExaminationsEnable
        {
            get;
            set;
        }

        public bool CSVToPatientsEnable
        {
            get;
            set;
        }
    }
}
