using Trainer.Application.Abstractions;
using Trainer.Application.Models;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.Examination.Queries.GetExaminations
{
    public class GetExaminationsQuery : GetPaginatedListBaseQuery<PaginatedList<Examination>>
    {
        public SortState SortOrder
        {
            get;
            set;
        }

        public GetExaminationsQuery(int? pageIndex, int? pageSize, SortState sortOrder) : base(pageIndex, pageSize)
        {
            SortOrder = sortOrder;
        }
    }
}
