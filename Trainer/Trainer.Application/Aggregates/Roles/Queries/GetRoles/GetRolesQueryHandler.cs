using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.Roles.Queries.GetRoles
{
    public class GetRolesQueryHandler : AbstractRequestHandler, IRequestHandler<GetRolesQuery, IEnumerable<Role>>
    {
        public GetRolesQueryHandler(
            Mediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<IEnumerable<Role>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
           var roles = await DbContext.Roles
                .ProjectTo<Role>(this.Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return roles;
        }
    }
}
