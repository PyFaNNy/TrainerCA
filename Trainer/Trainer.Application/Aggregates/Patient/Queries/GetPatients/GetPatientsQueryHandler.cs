﻿using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Trainer.Application.Abstractions;
using Trainer.Application.Extensions.IQueryableExtensions;
using Trainer.Application.Interfaces;
using Trainer.Application.Models;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.Patient.Queries.GetPatients
{
    public class GetPatientsQueryHandler : AbstractRequestHandler, IRequestHandler<GetPatientsQuery, PaginatedList<Patient>>
    {
        public GetPatientsQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<PaginatedList<Patient>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = DbContext.Patients
                .NotRemoved()
                .ProjectTo<Patient>(this.Mapper.ConfigurationProvider);

            var paginatedList = await PaginatedList<Patient>.CreateAsync(patients, request.PageIndex, request.PageSize);

            switch (request.SortOrder)
            {
                case SortState.FirstNameSort:
                    paginatedList.Items = paginatedList.Items.OrderBy(s => s.FirstName).ToList();
                    break;
                case SortState.FirstNameSortDesc:
                    paginatedList.Items = (List<Patient>)paginatedList.Items.OrderByDescending(s => s.FirstName).ToList();
                    break;
                case SortState.MiddleNameSortDesc:
                    paginatedList.Items = (List<Patient>)paginatedList.Items.OrderByDescending(s => s.MiddleName).ToList();
                    break;
                case SortState.MiddleNameSort:
                    paginatedList.Items = (List<Patient>)paginatedList.Items.OrderBy(s => s.MiddleName).ToList();
                    break;
                case SortState.LastNameSortDesc:
                    paginatedList.Items = (List<Patient>)paginatedList.Items.OrderByDescending(s => s.LastName).ToList();
                    break;
                case SortState.LastNameSort:
                    paginatedList.Items = (List<Patient>)paginatedList.Items.OrderBy(s => s.LastName).ToList();
                    break;
                case SortState.AgeSort:
                    paginatedList.Items = (List<Patient>)paginatedList.Items.OrderBy(s => s.Age).ToList();
                    break;
                case SortState.AgeSortDesc:
                    paginatedList.Items = (List<Patient>)paginatedList.Items.OrderByDescending(s => s.Age).ToList();
                    break;
                case SortState.SexSort:
                    paginatedList.Items = (List<Patient>)paginatedList.Items.OrderBy(s => s.Sex).ToList();
                    break;
                case SortState.SexSortDesc:
                    paginatedList.Items = (List<Patient>)paginatedList.Items.OrderByDescending(s => s.Sex).ToList();
                    break;
            }

            return paginatedList;
        }
    }
}
