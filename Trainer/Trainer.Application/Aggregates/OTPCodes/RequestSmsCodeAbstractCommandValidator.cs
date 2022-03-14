namespace Trainer.Application.Aggregates.OTPCodes
{
    using FluentValidation;

    public abstract class RequestSmsCodeAbstractCommandValidator<T>
        : AbstractValidator<T> where T : RequestSmsCodeAbstractCommand
    {
        public RequestSmsCodeAbstractCommandValidator()
        {
            this.RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"\+509\d{8}$");
        }
    }
}
