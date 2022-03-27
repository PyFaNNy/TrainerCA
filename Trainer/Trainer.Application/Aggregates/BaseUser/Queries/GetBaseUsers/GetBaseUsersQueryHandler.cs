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

namespace Trainer.Application.Aggregates.BaseUser.Queries.GetBaseUsers
{
    public class GetBaseUsersQueryHandler : AbstractRequestHandler, IRequestHandler<GetBaseUsersQuery, PaginatedList<BaseUser>>
    {
        private readonly BaseUserErrorSettings BaseUserErrorSettings;

        public GetBaseUsersQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IOptions<BaseUserErrorSettings> вaseUserErrorSettings)
            : base(mediator, dbContext, mapper)
        {
            BaseUserErrorSettings = вaseUserErrorSettings.Value;
        }

        public async Task<PaginatedList<BaseUser>> Handle(GetBaseUsersQuery request, CancellationToken cancellationToken)
        {
            if (BaseUserErrorSettings.GetBaseUsersEnable)
            {
                var baseUsers = DbContext.BaseUsers
                    .NotRemoved()
                    .ProjectTo<BaseUser>(this.Mapper.ConfigurationProvider);

                var paginatedList =
                    await PaginatedList<BaseUser>.CreateAsync(baseUsers, request.PageIndex, request.PageSize);

                switch (request.SortOrder)
                {
                    case SortState.EmailSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.Email).ToList();
                        break;
                    case SortState.EmailSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Email).ToList();
                        break;
                    case SortState.FirstNameSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.FirstName).ToList();
                        break;
                    case SortState.FirstNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.FirstName).ToList();
                        break;
                    case SortState.MiddleNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.MiddleName).ToList();
                        break;
                    case SortState.MiddleNameSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.MiddleName).ToList();
                        break;
                    case SortState.LastNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.LastName).ToList();
                        break;
                    case SortState.LastNameSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.LastName).ToList();
                        break;
                    case SortState.RoleSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Role).ToList();
                        break;
                    case SortState.RoleSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.Role).ToList();
                        break;
                    case SortState.StatusSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Status).ToList();
                        break;
                    case SortState.StatusSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.Status).ToList();
                        break;
                }

                return paginatedList;
            }

            if (BaseUserErrorSettings.GetRandomBaseUsersEnable)
            {
                Random rnd = new Random();
                var baseUsers = DbContext.BaseUsers
                    .NotRemoved()
                    .ProjectTo<BaseUser>(this.Mapper.ConfigurationProvider);

                baseUsers = baseUsers.Skip(rnd.Next(baseUsers.Count()));

                var paginatedList =
                    await PaginatedList<BaseUser>.CreateAsync(baseUsers, request.PageIndex, request.PageSize);

                switch (request.SortOrder)
                {
                    case SortState.EmailSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.Email).ToList();
                        break;
                    case SortState.EmailSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Email).ToList();
                        break;
                    case SortState.FirstNameSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.FirstName).ToList();
                        break;
                    case SortState.FirstNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.FirstName).ToList();
                        break;
                    case SortState.MiddleNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.MiddleName).ToList();
                        break;
                    case SortState.MiddleNameSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.MiddleName).ToList();
                        break;
                    case SortState.LastNameSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.LastName).ToList();
                        break;
                    case SortState.LastNameSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.LastName).ToList();
                        break;
                    case SortState.RoleSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Role).ToList();
                        break;
                    case SortState.RoleSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.Role).ToList();
                        break;
                    case SortState.StatusSortDesc:
                        paginatedList.Items = paginatedList.Items.OrderByDescending(s => s.Status).ToList();
                        break;
                    case SortState.StatusSort:
                        paginatedList.Items = paginatedList.Items.OrderBy(s => s.Status).ToList();
                        break;
                }

                return paginatedList;
            }

            return null;
        }
    }
}
