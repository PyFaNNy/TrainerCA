using MediatR;

namespace Trainer.Application.Aggregates.BaseUser.Commands.ResetPasswordUser
{
    public class ResetPasswordUserCommand : IRequest<Unit>
    {
        public string Email
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string ConfirmPassword
        {
            get;
            set;
        }
    }
}
