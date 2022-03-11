using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.Examination.Commands.FinishExamination
{
    public class FinishExaminationCommandHandler : AbstractRequestHandler, IRequestHandler<FinishExaminationCommand, Unit>
    {
        public FinishExaminationCommandHandler(
                IMediator mediator,
                ITrainerDbContext dbContext,
                IMapper mapper)
                : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(FinishExaminationCommand request, CancellationToken cancellationToken)
        {
            var examination = await this.DbContext.Examinations
                .Where(x => x.Id == request.ExaminationId)
                .FirstOrDefaultAsync(cancellationToken);

            if (examination == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Examination.Examination), request.ExaminationId);
            }

            examination.Status = Enums.ExaminationStatus.Finished;
            DbContext.Examinations.Update(examination);
            await DbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
