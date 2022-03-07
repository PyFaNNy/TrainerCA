using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.Patient.Commands.DeletePatient
{
    public class DeletePatientsCommandHandler : AbstractRequestHandler, IRequestHandler<DeletePatientsCommand, Unit>
    {
        public DeletePatientsCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(DeletePatientsCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.PatientsId)
            {
                var patient = await this.DbContext.Patients
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync(cancellationToken);

                if (patient == null)
                {
                    throw new NotFoundException(nameof(Domain.Entities.Patient.Patient), id);
                }

                this.DbContext.Patients.Remove(patient);
            }
            await this.DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
