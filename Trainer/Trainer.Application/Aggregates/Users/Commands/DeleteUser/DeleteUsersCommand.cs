using MediatR;

namespace Trainer.Application.Aggregates.Users.Commands.DeleteUser
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
