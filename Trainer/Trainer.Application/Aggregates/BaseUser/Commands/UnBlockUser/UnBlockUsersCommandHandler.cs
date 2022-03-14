using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.BaseUser.Commands.UnBlockUser
{
    public class UnBlockUsersCommandHandler : AbstractRequestHandler, IRequestHandler<UnBlockUsersCommand, Unit>
    {
        public UnBlockUsersCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(UnBlockUsersCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.UserIds)
            {
                var user = await this.DbContext.BaseUsers
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync(cancellationToken);

                if (user == null)
                {
                    throw new NotFoundException(nameof(Domain.Entities.BaseUser), id);
                }

                user.Status = Enums.StatusUser.Active;
            }
            await this.DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
