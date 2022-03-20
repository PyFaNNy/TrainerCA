using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.Patient.Commands.DeletePatient
{
    public class DeletePatientsCommandHandler : AbstractRequestHandler, IRequestHandler<DeletePatientsCommand, Unit>
    {
        private readonly PatientErrorSettings PatientErrorSettings;

        public DeletePatientsCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IOptions<PatientErrorSettings> patientErrorSettings)
            : base(mediator, dbContext, mapper)
        {
            PatientErrorSettings = patientErrorSettings.Value;
        }

        public async Task<Unit> Handle(DeletePatientsCommand request, CancellationToken cancellationToken)
        {
            if (PatientErrorSettings.DeletePatientEnable)
            {
                foreach (var id in request.PatientsId)
                {
                    var patient = await this.DbContext.Patients
                        .Where(x => x.Id == id)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (patient == null)
                    {
                        throw new NotFoundException(nameof(Domain.Entities.Patient.Patient), id);
                    }

                    patient.RemovedAt = DateTime.UtcNow;
                    DbContext.Patients.Update(patient);
                }
                await this.DbContext.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
