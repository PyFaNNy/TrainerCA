namespace Trainer.Settings.Error
{
    public class ExaminationErrorSettings : IApplicationSettings
    {
        public bool CreateExaminationEnable
        {
            get;
            set;
        }

        public bool CreateEmailExaminationEnable
        {
            get;
            set;
        }

        public bool DeleteExaminationEnable
        {
            get;
            set;
        }

        public bool FinishExaminationEnable
        {
            get;
            set;
        }

        public bool UpdateExaminationEnable
        {
            get;
            set;
        }

        public bool UpdateEmailExaminationEnable
        {
            get;
            set;
        }

        public bool GetExaminationEnable
        {
            get;
            set;
        }

        public bool GetRandomExaminationEnable
        {
            get;
            set;
        }

        public bool GetExaminationsEnable
        {
            get;
            set;
        }

        public bool GetRandomExaminationsEnable
        {
            get;
            set;
        }
    }
}
