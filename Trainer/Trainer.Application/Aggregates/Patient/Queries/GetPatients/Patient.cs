using Trainer.Application.Mappings;
using Trainer.Domain.Entities.Result;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.Patient.Queries.GetPatients
{
    public class Patient : IMapFrom<Domain.Entities.Patient.Patient>
    {
        public Guid Id
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string MiddleName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public int Age
        {
            get;
            set;
        }

        public Sex Sex
        {
            get;
            set;
        }
    }
}
