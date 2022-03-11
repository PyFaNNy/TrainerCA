using AutoMapper;
using MediatR;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.CSV.Commands.CSVToPatients
{
    public class CSVToPatientsCommandHandler : AbstractRequestHandler, IRequestHandler<CSVToPatientsCommand, Unit>
    {
        private readonly ICsvParserService CSVParserService;

        public CSVToPatientsCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            ICsvParserService csvParserService)
            : base(mediator, dbContext, mapper)
        {
            this.CSVParserService = csvParserService;
        }

        public async Task<Unit> Handle(CSVToPatientsCommand request, CancellationToken cancellationToken)
        {
            var patients = await CSVParserService.ReadCsvFileToPatient(request.CSVFile);

            await DbContext.Patients.AddRangeAsync(patients);
            await DbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
