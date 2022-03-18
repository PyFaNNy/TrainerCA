using MediatR;

namespace Trainer.Application.Aggregates.BaseUser.Queries.GetBaseUser
{
    public class GetBaseUserQuery : IRequest<BaseUser>
    {
        public string Email
        {
            get;
            set;
        }

        public GetBaseUserQuery(string email)
        {
            Email = email;
        }
    }
}
