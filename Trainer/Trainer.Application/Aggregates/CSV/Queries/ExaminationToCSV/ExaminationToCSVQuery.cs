using MediatR;

namespace Trainer.Application.Aggregates.CSV.Queries.ExaminationToCSV
{
    public class ExaminationToCSVQuery : IRequest<FileInfo>
    {
    }
}
