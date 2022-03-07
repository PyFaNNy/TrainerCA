using MediatR;

namespace Trainer.Application.Aggregates.CSV.Queries.PatientToCSV
{
    public class PatientToCSVQuery : IRequest<FileInfo>
    {
    }
}
