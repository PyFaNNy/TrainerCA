using AutoMapper;
using MediatR;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.Results.Commands.Create
{
    public class CreateResultsCommandHandler : AbstractRequestHandler, IRequestHandler<CreateResultsCommand, Unit>
    {
        public CreateResultsCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(CreateResultsCommand request, CancellationToken cancellationToken)
        {
            var result = Mapper.Map<Domain.Entities.Result.Result>(request);

            await DbContext.Results.AddAsync(result, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
