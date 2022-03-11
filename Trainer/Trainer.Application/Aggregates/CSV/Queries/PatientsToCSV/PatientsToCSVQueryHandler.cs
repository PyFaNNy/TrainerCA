using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.CSV.Queries.PatientsToCSV
{
    public class PatientsToCSVQueryHandler : AbstractRequestHandler, IRequestHandler<PatientsToCSVQuery, FileInfo>
    {
        private readonly ICsvParserService csvParserService;

        public PatientsToCSVQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            ICsvParserService csvParserService)
            : base(mediator, dbContext, mapper)
        {
            this.csvParserService = csvParserService;
        }

        public async Task<FileInfo> Handle(PatientsToCSVQuery request, CancellationToken cancellationToken)
        {
            var patients = await DbContext.Patients.ToListAsync(cancellationToken);

            return new FileInfo
            {
                FileName = $"Patients_{DateTime.UtcNow.Date}",
                Content = await csvParserService.WriteNewCsvFile(patients),
                Type = Enums.ContentType.CSV
            };
        }
    }
}
