using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.Examination.Commands.DeleteExamination
{
    public class DeleteExaminationsCommandHandler : AbstractRequestHandler, IRequestHandler<DeleteExaminationsCommand, Unit>
    {
        private readonly ExaminationErrorSettings ExaminationErrorSettings;

        public DeleteExaminationsCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IOptions<ExaminationErrorSettings> examinationErrorSettings)
            : base(mediator, dbContext, mapper)
        {
            ExaminationErrorSettings = examinationErrorSettings.Value;
        }

        public async Task<Unit> Handle(DeleteExaminationsCommand request, CancellationToken cancellationToken)
        {
            if (ExaminationErrorSettings.DeleteExaminationEnable)
            {
                foreach (var id in request.ExaminationsId)
                {
                    var examination = await this.DbContext.Examinations
                        .Where(x => x.Id == id)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (examination == null)
                    {
                        throw new NotFoundException(nameof(Domain.Entities.Examination.Examination), id);
                    }

                    this.DbContext.Examinations.Remove(examination);
                }
                await this.DbContext.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
