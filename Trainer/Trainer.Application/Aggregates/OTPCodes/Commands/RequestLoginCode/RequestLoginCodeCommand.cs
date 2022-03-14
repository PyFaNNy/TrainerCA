namespace Trainer.Application.Aggregates.OTPCodes.Commands.RequestLoginCode
{
    using MediatR;
    using Newtonsoft.Json;

    public class RequestLoginCodeCommand :  RequestSmsCodeAbstractCommand, IRequest<Unit>
    {
        public RequestLoginCodeCommand()
        {
            this.Action = Enums.OTPAction.Login;
        }

        [JsonProperty("password")]
        public string Password
        {
            get; set;
        }
    }
}
