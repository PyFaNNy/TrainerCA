using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Trainer.Application.Abstractions;
using Trainer.Application.Extensions.IQueryableExtensions;
using Trainer.Application.Interfaces;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.Patient.Queries.GetPatients
{
    public class GetPatientsQueryHandler : AbstractRequestHandler, IRequestHandler<GetPatientsQuery, IEnumerable<Patient>>
    {
        public GetPatientsQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<IEnumerable<Patient>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            var patient = DbContext.Patients
                .NotRemoved()
                .ProjectTo<Patient>(this.Mapper.ConfigurationProvider);

            switch (request.SortOrder)
            {
                case SortState.FirstNameSort:
                    patient = patient.OrderBy(s => s.FirstName);
                    break;
                case SortState.FirstNameSortDesc:
                    patient = patient.OrderByDescending(s => s.FirstName);
                    break;
                case SortState.MiddleNameSortDesc:
                    patient = patient.OrderByDescending(s => s.MiddleName);
                    break;
                case SortState.MiddleNameSort:
                    patient = patient.OrderBy(s => s.MiddleName);
                    break;
                case SortState.LastNameSortDesc:
                    patient = patient.OrderByDescending(s => s.LastName);
                    break;
                case SortState.LastNameSort:
                    patient = patient.OrderBy(s => s.LastName);
                    break;
                case SortState.AgeSort:
                    patient = patient.OrderBy(s => s.Age);
                    break;
                case SortState.AgeSortDesc:
                    patient = patient.OrderByDescending(s => s.Age);
                    break;
                case SortState.SexSort:
                    patient = patient.OrderBy(s => s.Sex);
                    break;
                case SortState.SexSortDesc:
                    patient = patient.OrderByDescending(s => s.Sex);
                    break;
            }

            return patient.ToList();
        }
    }
}
