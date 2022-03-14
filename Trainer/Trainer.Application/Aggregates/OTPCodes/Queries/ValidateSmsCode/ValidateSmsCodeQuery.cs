namespace Trainer.Application.Aggregates.OTPCodes.Queries.ValidateSmsCode
{
    using MediatR;
    using Newtonsoft.Json;
    using Trainer.Enums;

    public class ValidateSmsCodeQuery : IRequest<Code>
    {
        public ValidateSmsCodeQuery(string email, string code, OTPAction action)
        {
            this.Email = email;
            this.Code = code;
            this.Action = action;
        }

        [JsonProperty("email")]
        public string Email
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
