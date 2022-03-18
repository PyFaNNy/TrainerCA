using MediatR;

namespace Trainer.Application.Aggregates.BaseUser.Commands.DeclineUser
{
    public class DeclineUserCommand : IRequest<Unit>
    {
        public Guid UserId
        {
            get;
            set;
        }
    }
}
