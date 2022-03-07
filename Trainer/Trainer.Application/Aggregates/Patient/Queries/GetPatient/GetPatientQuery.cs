using MediatR;

namespace Trainer.Application.Aggregates.Patient.Queries.GetPatient
{
    public class GetPatientQuery : IRequest<Patient>
    {
        public Guid PatientId
        {
            get;
            set;
        }
    }
}
