using MediatR;

namespace Trainer.Application.Aggregates.Users.Commands.UnBlockUser
{
    public class UnBlockUsersCommand : IRequest<Unit>
    { 
        public Guid[] UserIds
        {
            get;
            set;
        }
    }
}
