using MediatR;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.Patient.Queries.GetPatients
{
    public class GetPatientsQuery : IRequest<IEnumerable<Patient>>
    {
        public SortState SortOrder
        {
            get;
            set;
        }
    }
}
