using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.Patient.Queries.GetPatient
{
    public class GetPatientQueryHandler : AbstractRequestHandler, IRequestHandler<GetPatientQuery, Patient>
    {
        public GetPatientQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Patient> Handle(GetPatientQuery request, CancellationToken cancellationToken)
        {
            var patient = DbContext.Patients
                .Where(x => x.Id == request.PatientId)
                .ProjectTo<Patient>(this.Mapper.ConfigurationProvider)
                .FirstOrDefault();

            return patient;
        }
    }
}
