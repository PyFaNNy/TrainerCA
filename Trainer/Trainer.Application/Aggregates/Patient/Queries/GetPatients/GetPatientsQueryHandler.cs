using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Extensions.IQueryableExtensions;
using Trainer.Application.Interfaces;
using Trainer.Application.Models;
using Trainer.Enums;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.Patient.Queries.GetPatients
{
    public class GetPatientsQueryHandler : AbstractRequestHandler, IRequestHandler<GetPatientsQuery, PaginatedList<Patient>>
    {
        private readonly PatientErrorSettings PatientErrorSettings;

        public GetPatientsQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IOptions<PatientErrorSettings> patientErrorSettings)
            : base(mediator, dbContext, mapper)
        {
            PatientErrorSettings = patientErrorSettings.Value;
        }

        public async Task<PaginatedList<Patient>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            if (PatientErrorSettings.GetPatientsEnable)
            {
                var patients = DbContext.Patients
                    .NotRemoved()
                    .ProjectTo<Patient>(this.Mapper.ConfigurationProvider);

                var paginatedList =
                    await PaginatedList<Patient>.CreateAsync(patients, request.PageIndex, request.PageSize);

                switch (request.SortOrder)
                {
                    case SortState.FirstNameSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.FirstName).ToList();
                        break;
                    case SortState.FirstNameSortDesc:
                        paginatedList.Items =
                            (List<Patient>) paginatedList.Items.OrderByDescending(s => s.FirstName).ToList();
                        break;
                    case SortState.MiddleNameSortDesc:
                        paginatedList.Items =
                            (List<Patient>) paginatedList.Items.OrderByDescending(s => s.MiddleName).ToList();
                        break;
                    case SortState.MiddleNameSort:
                        paginatedList.Items = (List<Patient>) paginatedList.Items.OrderBy(s => s.MiddleName).ToList();
                        break;
                    case SortState.LastNameSortDesc:
                        paginatedList.Items =
                            (List<Patient>) paginatedList.Items.OrderByDescending(s => s.LastName).ToList();
                        break;
                    case SortState.LastNameSort:
                        paginatedList.Items = (List<Patient>) paginatedList.Items.OrderBy(s => s.LastName).ToList();
                        break;
                    case SortState.AgeSort:
                        paginatedList.Items = (List<Patient>) paginatedList.Items.OrderBy(s => s.Age).ToList();
                        break;
                    case SortState.AgeSortDesc:
                        paginatedList.Items =
                            (List<Patient>) paginatedList.Items.OrderByDescending(s => s.Age).ToList();
                        break;
                    case SortState.SexSort:
                        paginatedList.Items = (List<Patient>) paginatedList.Items.OrderBy(s => s.Sex).ToList();
                        break;
                    case SortState.SexSortDesc:
                        paginatedList.Items =
                            (List<Patient>) paginatedList.Items.OrderByDescending(s => s.Sex).ToList();
                        break;
                }

                return paginatedList;
            }

            return null;
        }
    }
}
