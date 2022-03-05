using MediatR;

namespace Trainer.Application.Aggregates.Users.Commands.BlockUser
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
