using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.CSV.Commands.CSVToPatients
{
    public class CSVToPatientsCommandHandler : AbstractRequestHandler, IRequestHandler<CSVToPatientsCommand, Unit>
    {
        private readonly ICsvParserService CSVParserService;
        private readonly CSVErrorSettings CSVErrorSettings;

        public CSVToPatientsCommandHandler(
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

        public async Task<Unit> Handle(CSVToPatientsCommand request, CancellationToken cancellationToken)
        {
            if (CSVErrorSettings.CSVToPatientsEnable)
            {
                var patients = await CSVParserService.ReadCsvFileToPatient(request.CSVFile);

                await DbContext.Patients.AddRangeAsync(patients);
                await DbContext.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
