using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.BaseUser.Commands.SignIn
{
    public class SignInCommandHandler : AbstractRequestHandler, IRequestHandler<SignInCommand, Unit>
    {
        private readonly BaseUserErrorSettings BaseUserErrorSettings;

        public SignInCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IOptions<BaseUserErrorSettings> baseUserErrorSettings)
            : base(mediator, dbContext, mapper)
        {
            BaseUserErrorSettings = baseUserErrorSettings.Value;
        }

        public async Task<Unit> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            if (BaseUserErrorSettings.SignInEnable)
            {
                if (request.Role == Enums.UserRole.Doctor)
                {
                    var doctor = this.Mapper.Map<Domain.Entities.Doctor.Doctor>(request);

                    await this.DbContext.Doctors.AddAsync(doctor, cancellationToken);
                }

                if (request.Role == Enums.UserRole.Manager)
                {
                    var manager = this.Mapper.Map<Domain.Entities.Manager.Manager>(request);

                    await this.DbContext.Managers.AddAsync(manager, cancellationToken);
                }

                await this.DbContext.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
