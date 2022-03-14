namespace Trainer.Application.Aggregates.OTPCodes.Commands.RequestPassword
{
    using MediatR;
    using Newtonsoft.Json;

    public class RequestPasswordCommand : RequestSmsCodeAbstractCommand, IRequest<Unit>
    {
        public RequestPasswordCommand()
        {
            this.Action = Enums.OTPAction.ResetPassword;
        }
    }
}
