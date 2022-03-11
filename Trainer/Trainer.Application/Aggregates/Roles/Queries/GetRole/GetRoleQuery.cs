using MediatR;

namespace Trainer.Application.Aggregates.Roles.Queries.GetRole
{
    public class GetRoleQuery : IRequest<Role>
    {
        public Guid RoleId
        {
            get;
            set;
        }
    }
}
