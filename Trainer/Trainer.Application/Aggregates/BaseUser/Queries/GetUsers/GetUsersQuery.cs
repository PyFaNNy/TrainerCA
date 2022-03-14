using MediatR;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.BaseUser.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<IEnumerable<User>>
    {
        public SortState SortOrder
        {
            get;
            set;
        }
    }
}
