using MediatR;

namespace Trainer.Application.Aggregates.BaseUser.Commands.DeleteUser
{
    public class DeleteUsersCommand : IRequest<Unit>
    {
        public Guid[] UserIds
        {
            get;
            set;
        }
    }
}
