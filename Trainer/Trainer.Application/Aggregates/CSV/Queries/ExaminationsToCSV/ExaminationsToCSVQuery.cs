using MediatR;

namespace Trainer.Application.Aggregates.CSV.Queries.ExaminationsToCSV
{
    public class ExaminationsToCSVQuery : IRequest<FileInfo>
    {
    }
}
