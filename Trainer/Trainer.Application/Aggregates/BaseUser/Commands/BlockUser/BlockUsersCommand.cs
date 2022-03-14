using MediatR;

namespace Trainer.Application.Aggregates.BaseUser.Commands.BlockUser
{
    public class BlockUsersCommand : IRequest<Unit>
    {
        public Guid[] UserIds
        {
            get;
            set;
        }
    }
}
