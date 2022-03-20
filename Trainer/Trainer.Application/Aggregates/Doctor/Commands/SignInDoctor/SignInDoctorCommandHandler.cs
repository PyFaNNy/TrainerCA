using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.Doctor.Commands.SignInDoctor
{
    public class SignInDoctorCommandHandler : AbstractRequestHandler, IRequestHandler<SignInDoctorCommand, Unit>
    {
        private readonly DoctorErrorSettings DoctorErrorSettings;

        public SignInDoctorCommandHandler(
        IMediator mediator,
        ITrainerDbContext dbContext,
        IMapper mapper,
        IOptions<DoctorErrorSettings> doctorErrorSettings)
        : base(mediator, dbContext, mapper)
        {
            DoctorErrorSettings = doctorErrorSettings.Value;
        }

        public async Task<Unit> Handle(SignInDoctorCommand request, CancellationToken cancellationToken)
        {
            if (DoctorErrorSettings.SignInDoctorEnable)
            {
                var doctor = this.Mapper.Map<Domain.Entities.Doctor.Doctor>(request);

                await this.DbContext.Doctors.AddAsync(doctor, cancellationToken);
                await this.DbContext.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
