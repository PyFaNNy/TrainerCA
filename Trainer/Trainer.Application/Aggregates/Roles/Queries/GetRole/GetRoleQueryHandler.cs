using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.Roles.Queries.GetRole
{
    public class GetRoleQueryHandler : AbstractRequestHandler, IRequestHandler<GetRoleQuery, Role>
    {
        public GetRoleQueryHandler(
            Mediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Role> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var role = await DbContext.Roles
                .Where(r => r.Id == request.RoleId)
                .ProjectTo<Role>(this.Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (role == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Role.Role), request.RoleId);
            }

            return role;
        }
    }
}
