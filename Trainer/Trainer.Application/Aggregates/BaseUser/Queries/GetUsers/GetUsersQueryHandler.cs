using AutoMapper;
using MediatR;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.BaseUser.Queries.GetUsers
{
    public class GetUsersQueryHandler : AbstractRequestHandler, IRequestHandler<GetUsersQuery, IEnumerable<User>>
    {
        public GetUsersQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
