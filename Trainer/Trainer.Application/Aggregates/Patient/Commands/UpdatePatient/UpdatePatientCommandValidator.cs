using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.Patient.Commands.UpdatePatient
{
    public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
    {
        private ITrainerDbContext DbContext
        {
            get;
        }

        public UpdatePatientCommandValidator(ITrainerDbContext dbContext)
        {
            DbContext = dbContext;

            RuleFor(x => x.Email)
                .EmailAddress()
                .Must(IsUniqueEmail)
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

            RuleFor(x => x.Age)
                .ExclusiveBetween(1, 110)
                .WithMessage("Wrong age");
        }

        private bool IsUniqueEmail(string email)
        {
            return !this.DbContext.BaseUsers.Any(x => x.Email.Equals(email));
        }
    }
}
