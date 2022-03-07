using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.CSV.Queries.ExaminationToCSV
{
    public class ExaminationToCSVQueryHandler : AbstractRequestHandler, IRequestHandler<ExaminationToCSVQuery, FileInfo>
    {
        private readonly ICsvParserService csvParserService;

        public ExaminationToCSVQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            ICsvParserService csvParserService)
            : base(mediator, dbContext, mapper)
        {
            this.csvParserService = csvParserService;
        }

        public async Task<FileInfo> Handle(ExaminationToCSVQuery request, CancellationToken cancellationToken)
        {
            var examinations = await DbContext.Examinations.ToListAsync(cancellationToken);

            return new FileInfo
            {
                FileName =$"Examination_{DateTime.UtcNow.Date}",
                Content = await csvParserService.WriteNewCsvFile(examinations)
            };
        }
    }
}
