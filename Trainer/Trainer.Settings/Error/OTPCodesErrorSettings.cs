namespace Trainer.Settings.Error
{
    public class OTPCodesErrorSettings : IApplicationSettings
    {
        public bool RequestLoginCodeEnable
        {
            get;
            set;
        }

        public bool RequestRandomLoginCodeEnable
        {
            get;
            set;
        }

        public bool RequestPasswordEnable
        {
            get;
            set;
        }

        public bool RequestRandomPasswordEnable
        {
            get;
            set;
        }

        public bool RequestRegistrationCodeEnable
        {
            get;
            set;
        }

        public bool RequestRandomRegistrationCodeEnable
        {
            get;
            set;
        }

        public bool IsUniversalVerificationCodeEnabled
        {
            get;
            set;
        }

        public string UniversalVerificationCode
        {
            get;
            set;
        }
    }
}
