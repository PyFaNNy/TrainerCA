using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;
using Trainer.Application.Models;
using Trainer.Enums;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.Examination.Queries.GetExaminations
{
    public class GetExaminationsQueryHandler : AbstractRequestHandler, IRequestHandler<GetExaminationsQuery, PaginatedList<Examination>>
    {
        private readonly ExaminationErrorSettings ExaminationErrorSettings;
        public GetExaminationsQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IOptions<ExaminationErrorSettings> examinationErrorSettings)
            : base(mediator, dbContext, mapper)
        {
            ExaminationErrorSettings = examinationErrorSettings.Value;
        }

        public async Task<PaginatedList<Examination>> Handle(GetExaminationsQuery request, CancellationToken cancellationToken)
        {
            if (ExaminationErrorSettings.GetExaminationsEnable)
            {
                var examinations = DbContext.Examinations
                    .ProjectTo<Examination>(this.Mapper.ConfigurationProvider);

                var paginatedList = await PaginatedList<Examination>.CreateAsync(examinations, request.PageIndex, request.PageSize);

                switch (request.SortOrder)
                {
                    case SortState.DateSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(x => x.Date).ToList();
                        break;
                    case SortState.DateSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Date).ToList();
                        break;
                    case SortState.TypeSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.TypePhysicalActive).ToList();
                        break;
                    case SortState.TypeSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.TypePhysicalActive).ToList();
                        break;
                    case SortState.FirstNameSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.Patient.FirstName).ToList();
                        break;
                    case SortState.FirstNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Patient.FirstName).ToList();
                        break;
                    case SortState.MiddleNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Patient.MiddleName).ToList();
                        break;
                    case SortState.MiddleNameSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.Patient.MiddleName).ToList();
                        break;
                    case SortState.LastNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Patient.LastName).ToList();
                        break;
                    case SortState.LastNameSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.Patient.LastName).ToList();
                        break;
                }

                return paginatedList;
            }

            if (ExaminationErrorSettings.GetRandomExaminationsEnable)
            {
                Random rnd = new Random();
                var examinations = DbContext.Examinations
                    .ProjectTo<Examination>(this.Mapper.ConfigurationProvider);

                examinations = examinations.Skip(rnd.Next(examinations.Count()));

                var paginatedList = await PaginatedList<Examination>.CreateAsync(examinations, request.PageIndex, request.PageSize);

                switch (request.SortOrder)
                {
                    case SortState.DateSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(x => x.Date).ToList();
                        break;
                    case SortState.DateSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Date).ToList();
                        break;
                    case SortState.TypeSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.TypePhysicalActive).ToList();
                        break;
                    case SortState.TypeSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.TypePhysicalActive).ToList();
                        break;
                    case SortState.FirstNameSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.Patient.FirstName).ToList();
                        break;
                    case SortState.FirstNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Patient.FirstName).ToList();
                        break;
                    case SortState.MiddleNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Patient.MiddleName).ToList();
                        break;
                    case SortState.MiddleNameSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.Patient.MiddleName).ToList();
                        break;
                    case SortState.LastNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Patient.LastName).ToList();
                        break;
                    case SortState.LastNameSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.Patient.LastName).ToList();
                        break;
                }

                return paginatedList;
            }

            return null;
        }
    }
}
