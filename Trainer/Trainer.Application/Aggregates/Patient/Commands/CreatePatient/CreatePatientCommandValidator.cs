using FluentValidation;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.Patient.Commands.CreatePatient
{
    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        private ITrainerDbContext DbContext
        {
            get;
        }

        public CreatePatientCommandValidator(ITrainerDbContext dbContext)
        {
            DbContext = dbContext;

            RuleFor(x => x.Email)
                .EmailAddress()
                .Must(IsUniqueEmail)
                .WithMessage("Wrong email");

            RuleFor(x => x.LastName)
                 .NotNull()
                 .Matches(@"^([А-Я]{1}[а-яё]{1,49}|[A-Z]{1}[a-z]{1,49})$").WithMessage("Wrong last name");

            RuleFor(x => x.FirstName)
                 .NotNull()
                 .Matches(@"^([А-Я]{1}[а-яё]{1,49}|[A-Z]{1}[a-z]{1,49})$").WithMessage("Wrong first name");

            RuleFor(x => x.MiddleName)
                 .NotNull()
                 .Matches(@"^([А-Я]{1}[а-яё]{1,49}|[A-Z]{1}[a-z]{1,49})$").WithMessage("Wrong middle name");

            RuleFor(x => x.Age)
                .ExclusiveBetween(1, 110).WithMessage("Wrong age");
        }

        private bool IsUniqueEmail(string email)
        {
            return !this.DbContext.Patients.Any(x => x.Email.Equals(email));
        }
    }
}
