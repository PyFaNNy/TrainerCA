using MediatR;

namespace Trainer.Application.Aggregates.CSV.Queries.PatientsToCSV
{
    public class PatientsToCSVQuery : IRequest<FileInfo>
    {
    }
}
