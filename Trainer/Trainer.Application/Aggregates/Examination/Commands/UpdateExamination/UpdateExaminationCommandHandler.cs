using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.Examination.Commands.UpdateExamination
{
    public class UpdateExaminationCommandHandler : AbstractRequestHandler, IRequestHandler<UpdateExaminationCommand, Unit>
    {
        public UpdateExaminationCommandHandler(
        IMediator mediator,
        ITrainerDbContext dbContext,
        IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(UpdateExaminationCommand request, CancellationToken cancellationToken)
        {
            var examination = await this.DbContext.Examinations
                .Where(x => x.Id == request.ExaminationId)
                .FirstOrDefaultAsync(cancellationToken);

            if (examination == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Examination.Examination), request.ExaminationId);
            }

            this.Mapper.Map(request, examination);
            await this.DbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
