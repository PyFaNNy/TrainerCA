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
            BaseUser? baseUser = new BaseUser();
            if (BaseUserErrorSettings.GetBaseUserEnable)
            {
                baseUser = await DbContext.BaseUsers
                    .Where(x => x.Email == request.Email)
                    .NotRemoved()
                    .ProjectTo<BaseUser>(this.Mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);

                if (baseUser == null)
                {
                    throw new NotFoundException("User", request.Email);
                }

            }
            return baseUser;
        }
    }
}
