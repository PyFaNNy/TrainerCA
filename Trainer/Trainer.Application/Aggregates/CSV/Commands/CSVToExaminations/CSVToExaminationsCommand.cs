using MediatR;
using Microsoft.AspNetCore.Http;

namespace Trainer.Application.Aggregates.CSV.Commands.CSVToExaminations
{
    public class CSVToExaminationsCommand : IRequest<Unit>
    {
        public IFormFile CSVFile
        {
            get;
            set;
        }
    }
}
