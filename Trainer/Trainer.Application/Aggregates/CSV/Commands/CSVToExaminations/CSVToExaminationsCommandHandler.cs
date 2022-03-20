using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.CSV.Commands.CSVToExaminations
{
    public class CSVToExaminationsCommandHandler : AbstractRequestHandler, IRequestHandler<CSVToExaminationsCommand, Unit>
    {
        private readonly ICsvParserService CSVParserService;
        private readonly CSVErrorSettings CSVErrorSettings;

        public CSVToExaminationsCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            ICsvParserService csvParserService,
            IOptions<CSVErrorSettings> csvErrorSettings)
            : base(mediator, dbContext, mapper)
        {
            this.CSVParserService = csvParserService;
            CSVErrorSettings = csvErrorSettings.Value;
        }

        public async Task<Unit> Handle(CSVToExaminationsCommand request, CancellationToken cancellationToken)
        {
            if (CSVErrorSettings.CSVToExaminationsEnable)
            {
                var examinations = await CSVParserService.ReadCsvFileToExamination(request.CSVFile);

                await DbContext.Examinations.AddRangeAsync(examinations);
                await DbContext.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
