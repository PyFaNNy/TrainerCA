namespace Trainer.Application.Aggregates.OTPCodes.Commands.RequestRegistrationCode
{
    using MediatR;

    public class RequestRegistrationCodeCommand : RequestSmsCodeAbstractCommand, IRequest<Unit>
    {
        public RequestRegistrationCodeCommand()
        {
            this.Action = Enums.OTPAction.Registration;
        }
    }
}
