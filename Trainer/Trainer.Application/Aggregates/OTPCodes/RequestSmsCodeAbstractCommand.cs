namespace Trainer.Application.Aggregates.OTPCodes
{
    using Newtonsoft.Json;
    using Trainer.Enums;

    public abstract class RequestSmsCodeAbstractCommand
    {
        public string Email
        {
            get;
            set;
        }

        public string Host
        {
            get;
            set;
        }

        [JsonIgnore]
        public OTPAction Action
        {
            get;
            set;
        }
    }
}
