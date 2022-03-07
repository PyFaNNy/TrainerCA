using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.Examination.Queries.GetExaminations
{
    public class GetExaminationsQueryHandler : AbstractRequestHandler, IRequestHandler<GetExaminationsQuery, IEnumerable<Examination>>
    {
        public GetExaminationsQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<IEnumerable<Examination>> Handle(GetExaminationsQuery request, CancellationToken cancellationToken)
        {
            var examinations = DbContext.Examinations
                .ProjectTo<Examination>(this.Mapper.ConfigurationProvider);

            switch (request.SortOrder)
            {
                case SortState.DateSort:
                    examinations = examinations.OrderBy(s => s.Date);
                    break;
                case SortState.DateSortDesc:
                    examinations = examinations.OrderByDescending(s => s.Date);
                    break;
                case SortState.TypeSort:
                    examinations = examinations.OrderBy(s => s.TypePhysicalActive);
                    break;
                case SortState.TypeSortDesc:
                    examinations = examinations.OrderByDescending(s => s.TypePhysicalActive);
                    break;
                case SortState.FirstNameSort:
                    examinations = examinations.OrderBy(s => s.Patient.FirstName);
                    break;
                case SortState.FirstNameSortDesc:
                    examinations = examinations.OrderByDescending(s => s.Patient.FirstName);
                    break;
                case SortState.MiddleNameSortDesc:
                    examinations = examinations.OrderByDescending(s => s.Patient.MiddleName);
                    break;
                case SortState.MiddleNameSort:
                    examinations = examinations.OrderBy(s => s.Patient.MiddleName);
                    break;
                case SortState.LastNameSortDesc:
                    examinations = examinations.OrderByDescending(s => s.Patient.LastName);
                    break;
                case SortState.LastNameSort:
                    examinations = examinations.OrderBy(s => s.Patient.LastName);
                    break;
            }

            return examinations.ToList();
        }
    }
}
