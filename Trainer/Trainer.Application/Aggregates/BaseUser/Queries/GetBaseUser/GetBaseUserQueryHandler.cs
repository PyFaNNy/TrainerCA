using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Extensions.IQueryableExtensions;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.BaseUser.Queries.GetBaseUser
{
    public class GetBaseUserQueryHandler : AbstractRequestHandler, IRequestHandler<GetBaseUserQuery, BaseUser>
    {
        private readonly BaseUserErrorSettings BaseUserErrorSettings;

        public GetBaseUserQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IOptions<BaseUserErrorSettings> вaseUserErrorSettings)
            : base(mediator, dbContext, mapper)
        {
            BaseUserErrorSettings = вaseUserErrorSettings.Value;
        }

        public async Task<BaseUser> Handle(GetBaseUserQuery request, CancellationToken cancellationToken)
        {
            if (BaseUserErrorSettings.GetBaseUserEnable)
            {
                var baseUser = await DbContext.BaseUsers
                     .Where(x => x.Email == request.Email)
                     .NotRemoved()
                     .ProjectTo<BaseUser>(this.Mapper.ConfigurationProvider)
                     .FirstOrDefaultAsync(cancellationToken);

                if (baseUser == null)
                {
                    throw new NotFoundException("User", request.Email);
                }

                return baseUser;
            }

            if (BaseUserErrorSettings.GetRandomBaseUserEnable)
            {
                Random rnd = new Random();
                var baseUser = await DbContext.BaseUsers
                    .Skip(rnd.Next(30))
                    .NotRemoved()
                    .ProjectTo<BaseUser>(this.Mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);

                if (baseUser == null)
                {
                    throw new NotFoundException("User", request.Email);
                }

                return baseUser;
            }

            return null;
        }
    }
}
