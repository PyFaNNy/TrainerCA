using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Extensions.IQueryableExtensions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.BaseUser.Queries.GetBaseUser
{
    public class GetBaseUserQueryHandler : AbstractRequestHandler, IRequestHandler<GetBaseUserQuery, BaseUser>
    {
        public GetBaseUserQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<BaseUser> Handle(GetBaseUserQuery request, CancellationToken cancellationToken)
        {
            var baseUsers =await DbContext.BaseUsers
                .Where(x => x.Email == request.Email)
                .NotRemoved()
                .ProjectTo<BaseUser>(this.Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (baseUsers == null)
            {
                throw new NotFoundException("User", request.Email);
            }

            return baseUsers;
        }
    }
}
