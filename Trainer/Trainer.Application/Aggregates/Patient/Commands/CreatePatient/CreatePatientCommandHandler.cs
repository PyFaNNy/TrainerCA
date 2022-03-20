using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.Patient.Commands.CreatePatient
{
    public class CreatePatientCommandHandler : AbstractRequestHandler, IRequestHandler<CreatePatientCommand, Unit>
    {
        private readonly PatientErrorSettings PatientErrorSettings;

        public CreatePatientCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IOptions<PatientErrorSettings> patientErrorSettings)
            : base(mediator, dbContext, mapper)
        {
            PatientErrorSettings = patientErrorSettings.Value;
        }

        public async Task<Unit> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            if (PatientErrorSettings.CreatePatientEnable)
            {
                var patient = this.Mapper.Map<Domain.Entities.Patient.Patient>(request);

                await this.DbContext.Patients.AddAsync(patient, cancellationToken);
                await this.DbContext.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
