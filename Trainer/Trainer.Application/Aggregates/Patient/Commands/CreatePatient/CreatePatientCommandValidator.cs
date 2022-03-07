using FluentValidation;

namespace Trainer.Application.Aggregates.Patient.Commands.CreatePatient
{
    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
        {
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

            RuleFor(x => x.Age)
                .ExclusiveBetween(1, 110);
        }
    }
}
