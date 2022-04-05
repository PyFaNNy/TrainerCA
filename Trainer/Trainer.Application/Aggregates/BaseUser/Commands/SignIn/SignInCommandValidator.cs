using FluentValidation;
using Trainer.Application.Interfaces;
using Trainer.Common;

namespace Trainer.Application.Aggregates.BaseUser.Commands.SignIn
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        private ITrainerDbContext DbContext
        {
            get;
        }

        public SignInCommandValidator(ITrainerDbContext dbContext)
        {
            this.DbContext = dbContext;

            RuleFor(x => x.Email)
                .EmailAddress()
                .Must(this.IsUniqueEmail)
                .WithMessage("Wrong email");

            RuleFor(x => x.LastName)
                .NotNull()
                .Matches(@"^([А-Я]{1}[а-яё]{1,49}|[A-Z]{1}[a-z]{1,49})$")
                .WithMessage("Wrong last name");

            RuleFor(x => x.FirstName)
                .NotNull()
                .Matches(@"^([А-Я]{1}[а-яё]{1,49}|[A-Z]{1}[a-z]{1,49})$")
                .WithMessage("Wrong first name");

            RuleFor(x => x.MiddleName)
                .NotNull()
                .Matches(@"^([А-Я]{1}[а-яё]{1,49}|[A-Z]{1}[a-z]{1,49})$")
                .WithMessage("Wrong middle name");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .Must(PasswordsHelper.IsMeetsRequirements)
                .WithMessage("Wrong password");

            RuleFor(x => x.ConfirmPassword)
                .NotNull()
                .NotEmpty()
                .Equal(x => x.Password)
                .WithMessage("Wrong ConfirmPassword");
        }

        private bool IsUniqueEmail(string email)
        {
            return !this.DbContext.BaseUsers.Any(x => x.Email.Equals(email));
        }
    }
}
