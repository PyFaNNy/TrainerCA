using MediatR;

namespace Trainer.Application.Aggregates.BaseUser.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<Unit>
    {
        public string Email
        {
            get;
            set;
        }
    }
}
