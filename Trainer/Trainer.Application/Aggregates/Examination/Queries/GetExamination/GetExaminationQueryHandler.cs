using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.Examination.Queries.GetExamination
{
    public class GetExaminationQueryHandler : AbstractRequestHandler, IRequestHandler<GetExaminationQuery, Examination>
    {
        private readonly ExaminationErrorSettings ExaminationErrorSettings;
        public GetExaminationQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IOptions<ExaminationErrorSettings> examinationErrorSettings)
            : base(mediator, dbContext, mapper)
        {
            ExaminationErrorSettings = examinationErrorSettings.Value;
        }

        public async Task<Examination> Handle(GetExaminationQuery request, CancellationToken cancellationToken)
        {
            if (ExaminationErrorSettings.GetExaminationEnable)
            {
                var examination = DbContext.Examinations
                    .Where(x => x.Id == request.ExaminationId)
                    .ProjectTo<Examination>(this.Mapper.ConfigurationProvider)
                    .FirstOrDefault();

                InvCountIndicators(examination);

                return examination;
            }

            if (ExaminationErrorSettings.GetRandomExaminationEnable)
            {
                Random rnd = new Random();
                var examination = DbContext.Examinations
                    .Skip(rnd.Next(30))
                    .ProjectTo<Examination>(this.Mapper.ConfigurationProvider)
                    .FirstOrDefault();

                InvCountIndicators(examination);

                return examination;
            }

            return null;
        }

        private void InvCountIndicators(Examination model)
        {
            var temp = model.Indicators;
            if (temp - 8 >= 0)
            {
                temp -= 8;
                model.Indicator4 = true;
            }
            if (temp - 4 >= 0)
            {
                temp -= 4;
                model.Indicator3 = true;
            }
            if (temp - 2 >= 0)
            {
                temp -= 2;
                model.Indicator2 = true;
            }
            if (temp - 1 >= 0)
            {
                temp -= 1;
                model.Indicator1 = true;
            }
        }
    }
}
