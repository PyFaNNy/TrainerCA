using AutoMapper;
using MediatR;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.Patient.Commands.CreatePatient
{
    public class CreatePatientCommandHandler : AbstractRequestHandler, IRequestHandler<CreatePatientCommand, Unit>
    {
        public CreatePatientCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = this.Mapper.Map<Domain.Entities.Patient.Patient>(request);

            await this.DbContext.Patients.AddAsync(patient, cancellationToken);
            await this.DbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
