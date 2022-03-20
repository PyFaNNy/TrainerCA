using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.Manager.Commands.SignInManager
{
    public class SignInManagerCommandHandler : AbstractRequestHandler, IRequestHandler<SignInManagerCommand, Unit>
    {
        private readonly ManagerErrorSettings ManagerErrorSettings;

        public SignInManagerCommandHandler(
        IMediator mediator,
        ITrainerDbContext dbContext,
        IMapper mapper,
        IOptions<ManagerErrorSettings> managerErrorSettings)
        : base(mediator, dbContext, mapper)
        {
            ManagerErrorSettings = managerErrorSettings.Value;
        }

        public async Task<Unit> Handle(SignInManagerCommand request, CancellationToken cancellationToken)
        {
            if (ManagerErrorSettings.SignInManagerEnable)
            {
                var manager = this.Mapper.Map<Domain.Entities.Manager.Manager>(request);

                await this.DbContext.Managers.AddAsync(manager, cancellationToken);
                await this.DbContext.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
