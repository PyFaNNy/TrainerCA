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

        public Task<Unit> Handle(SignInManagerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
