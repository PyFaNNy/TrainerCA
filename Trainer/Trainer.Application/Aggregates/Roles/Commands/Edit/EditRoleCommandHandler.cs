using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.Roles.Commands.Edit
{
    public class EditRoleCommandHandler : AbstractRequestHandler, IRequestHandler<EditRoleCommand, Unit>
    {
        public EditRoleCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await this.DbContext.Roles
                .Where(x => x.Id == request.RoleId)
                .FirstOrDefaultAsync(cancellationToken);

            if (role == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Role.Role), request.RoleId);
            }

            this.Mapper.Map(request, role);
            await this.DbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
