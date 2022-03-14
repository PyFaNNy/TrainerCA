using MediatR;

namespace Trainer.Application.Aggregates.BaseUser.Commands.UnBlockUser
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
