using FluentValidation;
using Trainer.Application.Interfaces;
using Trainer.Common;

namespace Trainer.Application.Aggregates.Users.Commands.SignInUser
{
    public class SignInUserCommandValidator : AbstractValidator<SignInUserCommand>
    {
        private ITrainerDbContext DbContext
        {
            get;
        }

        public SignInUserCommandValidator(ITrainerDbContext dbContext)
        {
            this.DbContext = dbContext;

            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.LastName)
                 .NotNull()
                 .Matches(@"^([А-Я]{1}[а-яё]{1,49}|[A-Z]{1}[a-z]{1,49})$");

            RuleFor(x => x.FirstName)
                 .NotNull()
                 .Matches(@"^([А-Я]{1}[а-яё]{1,49}|[A-Z]{1}[a-z]{1,49})$");

            RuleFor(x => x.MiddleName)
                 .NotNull()
                 .Matches(@"^([А-Я]{1}[а-яё]{1,49}|[A-Z]{1}[a-z]{1,49})$");

            RuleFor(x => x.Password)
                .Must(PasswordsHelper.IsMeetsRequirements);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.Password);

            RuleFor(x => x.UserName)
                .NotEmpty()
                .Must(this.IsUniqueUserName);
        }

        private bool IsUniqueUserName(string userName)
        {
            return !this.DbContext.Users.Any(x => x.UserName.Equals(userName));
        }
    }
}
