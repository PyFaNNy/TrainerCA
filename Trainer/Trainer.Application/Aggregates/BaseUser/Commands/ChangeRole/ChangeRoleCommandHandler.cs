using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.BaseUser.Commands.ChangeRole
{
    public class ChangeRoleCommandHandler : AbstractRequestHandler, IRequestHandler<ChangeRoleCommand, Unit>
    {
        public ChangeRoleCommandHandler(
                IMediator mediator,
                ITrainerDbContext dbContext,
                IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await DbContext.BaseUsers
                .Where(x => x.Id == request.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.BaseUser), request.UserId);
            }

            return Unit.Value; 
        }
    }
}
