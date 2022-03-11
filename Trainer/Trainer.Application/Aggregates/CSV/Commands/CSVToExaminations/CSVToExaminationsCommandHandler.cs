using AutoMapper;
using MediatR;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.CSV.Commands.CSVToExaminations
{
    public class CSVToExaminationsCommandHandler : AbstractRequestHandler, IRequestHandler<CSVToExaminationsCommand, Unit>
    {
        private readonly ICsvParserService CSVParserService;

        public CSVToExaminationsCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            ICsvParserService csvParserService)
            : base(mediator, dbContext, mapper)
        {
            this.CSVParserService = csvParserService;
        }

        public async Task<Unit> Handle(CSVToExaminationsCommand request, CancellationToken cancellationToken)
        {
            var examinations =await CSVParserService.ReadCsvFileToExamination(request.CSVFile);

            await DbContext.Examinations.AddRangeAsync(examinations);
            await DbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
