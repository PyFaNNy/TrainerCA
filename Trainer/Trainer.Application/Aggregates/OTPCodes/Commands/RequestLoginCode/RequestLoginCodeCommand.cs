namespace Trainer.Application.Aggregates.OTPCodes.Commands.RequestLoginCode
{
    using MediatR;

    public class RequestLoginCodeCommand :  RequestSmsCodeAbstractCommand, IRequest<Unit>
    {
        public RequestLoginCodeCommand()
        {
            this.Action = Enums.OTPAction.Login;
        }
    }
}
