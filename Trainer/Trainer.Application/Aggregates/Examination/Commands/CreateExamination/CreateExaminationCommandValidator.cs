using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Trainer.Application.Aggregates.Examination.Commands.CreateExamination
{
    public class CreateExaminationCommandValidator : AbstractValidator<CreateExaminationCommand>
    {
        public CreateExaminationCommandValidator(IStringLocalizer<CreateExaminationCommandValidator> localizer)
        {
            RuleFor(x => x.Date)
                .Must(ValidateDate)
                .WithMessage("Wrong date");
        }

        private bool ValidateDate(DateTime date)
        {
            return date > DateTime.Now;
        }
    }
}
