using MediatR;

namespace Trainer.Application.Aggregates.Examination.Commands.DeleteExamination
{
    public class DeleteExaminationsCommand : IRequest<Unit>
    {
        public Guid[] ExaminationsId
        {
            get;
            set;
        }
    }
}
