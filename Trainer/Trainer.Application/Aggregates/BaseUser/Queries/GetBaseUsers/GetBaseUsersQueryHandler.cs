using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Trainer.Application.Abstractions;
using Trainer.Application.Extensions.IQueryableExtensions;
using Trainer.Application.Interfaces;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.BaseUser.Queries.GetBaseUsers
{
    public class GetBaseUsersQueryHandler : AbstractRequestHandler, IRequestHandler<GetBaseUsersQuery, IEnumerable<BaseUser>>
    {
        public GetBaseUsersQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<IEnumerable<BaseUser>> Handle(GetBaseUsersQuery request, CancellationToken cancellationToken)
        {
            var baseUsers = DbContext.BaseUsers
                .NotRemoved()
                .ProjectTo<BaseUser>(this.Mapper.ConfigurationProvider);

            switch (request.SortOrder)
            {
                case SortState.EmailSort:
                    baseUsers = baseUsers.OrderBy(s => s.Email);
                    break;
                case SortState.EmailSortDesc:
                    baseUsers = baseUsers.OrderByDescending(s => s.Email);
                    break;
                case SortState.FirstNameSort:
                    baseUsers = baseUsers.OrderBy(s => s.FirstName);
                    break;
                case SortState.FirstNameSortDesc:
                    baseUsers = baseUsers.OrderByDescending(s => s.FirstName);
                    break;
                case SortState.MiddleNameSortDesc:
                    baseUsers = baseUsers.OrderByDescending(s => s.MiddleName);
                    break;
                case SortState.MiddleNameSort:
                    baseUsers = baseUsers.OrderBy(s => s.MiddleName);
                    break;
                case SortState.LastNameSortDesc:
                    baseUsers = baseUsers.OrderByDescending(s => s.LastName);
                    break;
                case SortState.LastNameSort:
                    baseUsers = baseUsers.OrderBy(s => s.LastName);
                    break;
                case SortState.RoleSortDesc:
                    baseUsers = baseUsers.OrderByDescending(s => s.Role);
                    break;
                case SortState.RoleSort:
                    baseUsers = baseUsers.OrderBy(s => s.Role);
                    break;
                case SortState.StatusSortDesc:
                    baseUsers = baseUsers.OrderByDescending(s => s.Status);
                    break;
                case SortState.StatusSort:
                    baseUsers = baseUsers.OrderBy(s => s.Status);
                    break;
            }

            return baseUsers.ToList();
        }
    }
}
