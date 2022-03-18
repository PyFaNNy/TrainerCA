using MediatR;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.BaseUser.Queries.GetBaseUsers
{
    public class GetBaseUsersQuery : IRequest<IEnumerable<BaseUser>>
    {
        public SortState SortOrder
        {
            get;
            set;
        }
    }
}
