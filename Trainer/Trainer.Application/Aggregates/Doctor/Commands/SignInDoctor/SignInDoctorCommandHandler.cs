using AutoMapper;
using MediatR;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.Doctor.Commands.SignInDoctor
{
    public class SignInDoctorCommandHandler : AbstractRequestHandler, IRequestHandler<SignInDoctorCommand, Unit>
    {
        public SignInDoctorCommandHandler(
        IMediator mediator,
        ITrainerDbContext dbContext,
        IMapper mapper)
        : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(SignInDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = this.Mapper.Map<Domain.Entities.Doctor.Doctor>(request);

            await this.DbContext.Doctors.AddAsync(doctor, cancellationToken);
            await this.DbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
