using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Extensions.IQueryableExtensions;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.Patient.Queries.GetPatient
{
    public class GetPatientQueryHandler : AbstractRequestHandler, IRequestHandler<GetPatientQuery, Patient>
    {
        private readonly PatientErrorSettings PatientErrorSettings;

        public GetPatientQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IOptions<PatientErrorSettings> patientErrorSettings)
            : base(mediator, dbContext, mapper)
        {
            PatientErrorSettings = patientErrorSettings.Value;
        }

        public async Task<Patient> Handle(GetPatientQuery request, CancellationToken cancellationToken)
        {
            if (PatientErrorSettings.GetPatientEnable)
            {
                var patient = DbContext.Patients
                    .Where(x => x.Id == request.PatientId)
                    .NotRemoved()
                    .ProjectTo<Patient>(this.Mapper.ConfigurationProvider)
                    .FirstOrDefault();

                return patient;
            }

            if (PatientErrorSettings.GetRandomPatientEnable)
            {
                Random rnd = new Random();
                var patient = DbContext.Patients
                    .Skip(rnd.Next(30))
                    .NotRemoved()
                    .ProjectTo<Patient>(this.Mapper.ConfigurationProvider)
                    .FirstOrDefault();

                return patient;
            }

            return null;
        }
    }
}
