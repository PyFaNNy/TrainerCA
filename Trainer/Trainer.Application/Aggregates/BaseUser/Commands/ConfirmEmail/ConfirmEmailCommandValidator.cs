using FluentValidation;

namespace Trainer.Application.Aggregates.BaseUser.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress();
        }
    }
}
