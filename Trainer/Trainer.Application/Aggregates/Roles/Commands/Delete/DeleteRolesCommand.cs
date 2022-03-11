using MediatR;

namespace Trainer.Application.Aggregates.Roles.Commands.Delete
{
    public class DeleteRolesCommand : IRequest<Unit>
    {
        public IEnumerable<Guid> Ids
        {
            get;
            set;
        }
    }
}
