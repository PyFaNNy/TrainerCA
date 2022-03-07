using FluentValidation;

namespace Trainer.Application.Aggregates.Examination.Commands.CreateExamination
{
    public class CreateExaminationCommandValidator : AbstractValidator<CreateExaminationCommand>
    {
        public CreateExaminationCommandValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty();

            RuleFor(x => x.TypePhysicalActive)
                 .NotEmpty();

            RuleFor(x => x.Indicators)
                .GreaterThan(0);
        }
    }
}
