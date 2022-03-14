using FluentValidation;
using Trainer.Application.Interfaces;
using Trainer.Common;

namespace Trainer.Application.Aggregates.Doctor.Commands.SignInDoctor
{
    public class SignInDoctorCommandValidator : AbstractValidator<SignInDoctorCommand>
    {
        private ITrainerDbContext DbContext
        {
            get;
        }

        public SignInDoctorCommandValidator(ITrainerDbContext dbContext)
        {
            this.DbContext = dbContext;

            RuleFor(x => x.Email)
                .EmailAddress()
                .Must(this.IsUniqueEmail);

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
        }

        private bool IsUniqueEmail(string email)
        {
            return !this.DbContext.BaseUsers.Any(x => x.Email.Equals(email));
        }
    }
}
