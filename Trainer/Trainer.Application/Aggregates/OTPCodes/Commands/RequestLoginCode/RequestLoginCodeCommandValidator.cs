namespace Trainer.Application.Aggregates.OTPCodes.Commands.RequestLoginCode
{
    using FluentValidation;
    using MediatR;

    public class RequestLoginCodeCommandValidator
        : RequestSmsCodeAbstractCommandValidator<RequestLoginCodeCommand>
    {
        public RequestLoginCodeCommandValidator()
            : base()
        {
        }
    }
}
