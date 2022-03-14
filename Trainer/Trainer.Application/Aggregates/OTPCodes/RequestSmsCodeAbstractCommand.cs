namespace Trainer.Application.Aggregates.OTPCodes
{
    using Newtonsoft.Json;
    using Trainer.Enums;

    public abstract class RequestSmsCodeAbstractCommand
    {
        [JsonProperty("email")]
        public string Email
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
