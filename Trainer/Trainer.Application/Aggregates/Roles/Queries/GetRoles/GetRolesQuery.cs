using MediatR;

namespace Trainer.Application.Aggregates.Roles.Queries.GetRoles
{
    public class GetRolesQuery : IRequest<IEnumerable<Role>>
    {
    }
}
