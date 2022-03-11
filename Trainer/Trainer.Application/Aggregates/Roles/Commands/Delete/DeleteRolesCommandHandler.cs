using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.Roles.Commands.Delete
{
    public class DeleteRolesCommandHandler : AbstractRequestHandler, IRequestHandler<DeleteRolesCommand, Unit>
    {
        public DeleteRolesCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(DeleteRolesCommand request, CancellationToken cancellationToken)
        {
            var roles = await DbContext.Roles
                .Where(x => request.Ids.Contains(x.Id))
                .ToListAsync(cancellationToken);

            DbContext.Roles.RemoveRange(roles);
            DbContext.SaveChanges();

            return Unit.Value;
        }
    }
}
