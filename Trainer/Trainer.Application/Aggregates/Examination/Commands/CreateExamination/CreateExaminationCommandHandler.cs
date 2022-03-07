using AutoMapper;
using MediatR;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.Examination.Commands.CreateExamination
{
    public class CreateExaminationCommandHandler : AbstractRequestHandler, IRequestHandler<CreateExaminationCommand, Unit>
    {
        public CreateExaminationCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(CreateExaminationCommand request, CancellationToken cancellationToken)
        {
            var examinaton = this.Mapper.Map<Domain.Entities.Examination.Examination>(request);

            await this.DbContext.Examinations.AddAsync(examinaton, cancellationToken);
            await this.DbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
