using Trainer.Application.Abstractions;
using Trainer.Application.Models;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.BaseUser.Queries.GetBaseUsers
{
    public class GetBaseUsersQuery : GetPaginatedListBaseQuery<PaginatedList<BaseUser>>
    {

        public SortState SortOrder
        {
            get;
            set;
        }

        public GetBaseUsersQuery(int? pageIndex, int? pageSize, SortState sortOrder) : base(pageIndex, pageSize)
        {
            SortOrder = sortOrder;
        }
    }
}
