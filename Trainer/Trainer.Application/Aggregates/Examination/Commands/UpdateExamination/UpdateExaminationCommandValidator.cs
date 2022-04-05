using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Trainer.Application.Aggregates.Examination.Commands.UpdateExamination
{
    public class UpdateExaminationCommandValidator : AbstractValidator<UpdateExaminationCommand>
    {
        public UpdateExaminationCommandValidator()
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
