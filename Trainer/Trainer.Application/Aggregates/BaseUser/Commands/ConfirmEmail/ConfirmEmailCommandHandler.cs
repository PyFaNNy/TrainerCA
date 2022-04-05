using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.BaseUser.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : AbstractRequestHandler, IRequestHandler<ConfirmEmailCommand, Unit>
    {
        public ConfirmEmailCommandHandler(
                IMediator mediator,
                ITrainerDbContext dbContext,
                IMapper mapper)
                : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user =await DbContext.BaseUsers
            .Where(x => x.Email == request.Email)
            .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.BaseUser), request.Email);
            }

            user.EmailConfirmed = true;
            user.Status = Enums.StatusUser.Pending;

            await DbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
