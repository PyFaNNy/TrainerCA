using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainer.Application.Aggregates.Examination.Commands.FinishExamination
{
    public class FinishExaminationCommand : IRequest<Unit>
    {
        public Guid ExaminationId
        {
            get;
            set;
        }
    }
}
