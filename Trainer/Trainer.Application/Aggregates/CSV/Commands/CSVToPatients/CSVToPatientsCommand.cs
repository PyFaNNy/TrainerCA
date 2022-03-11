using MediatR;
using Microsoft.AspNetCore.Http;

namespace Trainer.Application.Aggregates.CSV.Commands.CSVToPatients
{
    public class CSVToPatientsCommand : IRequest<Unit>
    {
        public IFormFile CSVFile
        {
            get;
            set;
        }
    }
}
