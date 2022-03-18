using MediatR;

namespace Trainer.Application.Aggregates.BaseUser.Commands.ApproveUser
{
    public class ApproveUserCommand : IRequest<Unit>
    {
        public Guid UserId
        {
            get;
            set;
        }
    }
}
