namespace Trainer.Application.Aggregates.OTPCodes.Queries.ValidateSmsCode
{
    using Prixy.Enums;
    using MediatR;
    using Newtonsoft.Json;
    using Trainer.Enums;

    public class ValidateSmsCodeQuery : IRequest<Code>
    {
        public ValidateSmsCodeQuery(string phone, string code, OTPAction action)
        {
            this.PhoneNumber = phone;
            this.Code = code;
            this.Action = action;
        }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber
        {
            get;
        }

        [JsonProperty("code")]
        public string Code
        {
            get; 
        }

        [JsonIgnore]
        public OTPAction Action
        {
            get;
        }
    }
}
