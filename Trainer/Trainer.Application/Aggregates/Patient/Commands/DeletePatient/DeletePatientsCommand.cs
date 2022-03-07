using MediatR;

namespace Trainer.Application.Aggregates.Patient.Commands.DeletePatient
{
    public class DeletePatientsCommand : IRequest<Unit>
    {
        public Guid[] PatientsId
        {
            get;
            set;
        }
    }
}
