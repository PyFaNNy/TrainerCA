using AutoMapper;
using MediatR;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.Manager.Commands.SignInManager
{
    public class SignInManagerCommandHandler : AbstractRequestHandler, IRequestHandler<SignInManagerCommand, Unit>
    {
        public SignInManagerCommandHandler(
        IMediator mediator,
        ITrainerDbContext dbContext,
        IMapper mapper)
        : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(SignInManagerCommand request, CancellationToken cancellationToken)
        {
            var manager = this.Mapper.Map<Domain.Entities.Manager.Manager>(request);

            await this.DbContext.Managers.AddAsync(manager, cancellationToken);
            await this.DbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
