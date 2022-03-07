using MediatR;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.Examination.Queries.GetExaminations
{
    public class GetExaminationsQuery : IRequest<IEnumerable<Examination>>
    {
        public SortState SortOrder
        {
            get;
            set;
        }
    }
}
