using Trainer.Application.Abstractions;
using Trainer.Application.Models;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.Patient.Queries.GetPatients
{
    public class GetPatientsQuery : GetPaginatedListBaseQuery<PaginatedList<Patient>>
    {
        public SortState SortOrder
        {
            get;
            set;
        }

        public GetPatientsQuery(int? pageIndex, int? pageSize, SortState sortOrder) : base(pageIndex, pageSize)
        {
            SortOrder = sortOrder;
        }
    }
}
