namespace Trainer.Settings.Error
{
    public class BaseUserErrorSettings : IApplicationSettings
    {
        public bool ApproveUserEnable
        {
            get;
            set;
        }

        public bool ApproveUserEmailEnable
        {
            get;
            set;
        }

        public bool BlockUserEnable
        {
            get;
            set;
        }

        public bool BlockUserEmailEnable
        {
            get;
            set;
        }

        public bool ChangeRoleEnable
        {
            get;
            set;
        }

        public bool DeclineUserEnable
        {
            get;
            set;
        }

        public bool DeclineUserEmailEnable
        {
            get;
            set;
        }

        public bool DeleteUserEnable
        {
            get;
            set;
        }

        public bool DeleteUserEmailEnable
        {
            get;
            set;
        }

        public bool UnBlockUserEnable
        {
            get;
            set;
        }

        public bool UnBlockUserEmailEnable
        {
            get;
            set;
        }

        public bool ResetPasswordUserEnable
        {
            get;
            set;
        }

        public bool GetBaseUserEnable
        {
            get;
            set;
        }

        public bool GetRandomBaseUserEnable
        {
            get;
            set;
        }

        public bool GetBaseUsersEnable
        {
            get;
            set;
        }

        public bool GetRandomBaseUsersEnable
        {
            get;
            set;
        }

        public bool SignInEnable
        {
            get;
            set;
        }
    }
}