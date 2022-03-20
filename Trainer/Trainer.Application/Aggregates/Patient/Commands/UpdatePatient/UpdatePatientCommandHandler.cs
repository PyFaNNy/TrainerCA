using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.Patient.Commands.UpdatePatient
{
    public class UpdatePatientCommandHandler : AbstractRequestHandler, IRequestHandler<UpdatePatientCommand, Unit>
    {
        private readonly PatientErrorSettings PatientErrorSettings;

        public UpdatePatientCommandHandler(
        IMediator mediator,
        ITrainerDbContext dbContext,
        IMapper mapper,
        IOptions<PatientErrorSettings> patientErrorSettings)
        : base(mediator, dbContext, mapper)
        {
            PatientErrorSettings = patientErrorSettings.Value;
        }

        public async Task<Unit> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            if (PatientErrorSettings.UpdatePatientEnable)
            {
                var patient = await this.DbContext.Patients
                .Where(x => x.Id == request.PatientId)
                .FirstOrDefaultAsync(cancellationToken);

                if (patient == null)
                {
                    throw new NotFoundException(nameof(Domain.Entities.Patient.Patient), request.PatientId);
                }

                this.Mapper.Map(request, patient);
                await this.DbContext.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
