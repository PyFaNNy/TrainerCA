using MediatR;

namespace Trainer.Application.Aggregates.Examination.Queries.GetExamination
{
    public class GetExaminationQuery : IRequest<Examination>
    {
        public Guid ExaminationId
        {
            get;
            set;
        }
    }
}
