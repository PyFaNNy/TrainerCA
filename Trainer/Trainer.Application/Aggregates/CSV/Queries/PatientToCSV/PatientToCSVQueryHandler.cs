using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.CSV.Queries.PatientToCSV
{
    public class PatientToCSVQueryHandler : AbstractRequestHandler, IRequestHandler<PatientToCSVQuery, FileInfo>
    {
        private readonly ICsvParserService csvParserService;

        public PatientToCSVQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            ICsvParserService csvParserService)
            : base(mediator, dbContext, mapper)
        {
            this.csvParserService = csvParserService;
        }

        public async Task<FileInfo> Handle(PatientToCSVQuery request, CancellationToken cancellationToken)
        {
            var patients = await DbContext.Patients.ToListAsync(cancellationToken);

            return new FileInfo
            {
                FileName = $"Patients_{DateTime.UtcNow.Date}",
                Content = await csvParserService.WriteNewCsvFile(patients)
            };
        }
    }
}
