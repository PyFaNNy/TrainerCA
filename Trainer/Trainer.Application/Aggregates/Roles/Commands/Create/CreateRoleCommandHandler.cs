using AutoMapper;
using MediatR;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;
using Trainer.Domain.Entities.Role;

namespace Trainer.Application.Aggregates.Roles.Commands.Create
{
    public class CreateRoleCommandHandler : AbstractRequestHandler, IRequestHandler<CreateRoleCommand, Unit>
    {
        public CreateRoleCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = Mapper.Map<Role>(request);

            DbContext.Roles.Add(role);
            DbContext.SaveChanges();

            return Unit.Value;
        }
    }
}
