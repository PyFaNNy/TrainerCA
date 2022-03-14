using MediatR;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.BaseUser.Commands.ChangeRole
{
    public class ChangeRoleCommand : IRequest<Unit>
    {
        public Guid UserId
        {
            get;
            set;
        }

        public UserRole Role
        {
            get;
            set;
        }
    }
}
