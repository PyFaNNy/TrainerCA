using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainer.Application.Aggregates.Examination.Commands.UpdateExamination
{
    public class UpdateExaminationCommandValidator : AbstractValidator<UpdateExaminationCommand>
    {
        public UpdateExaminationCommandValidator()
        {
            RuleFor(x => x.TypePhysicalActive)
                 .NotEmpty();
        }
    }
}
