namespace Trainer.Application.Aggregates.OTPCodes.Commands.RequestRegistrationCode
{
    using AutoMapper;
    using Interfaces;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class RequestRegistrationCodeCommandHandler : RequestSmsCodeAbstractCommandHandler, IRequestHandler<RequestRegistrationCodeCommand, Unit>
    {
        public RequestRegistrationCodeCommandHandler(IMediator mediator, ITrainerDbContext dbContext, IMapper mapper, IMailService emailService)
    : base(mediator, dbContext, mapper, emailService)
        {
        }

        public async Task<Unit> Handle(RequestRegistrationCodeCommand request, CancellationToken cancellationToken)
        {
            this.LimitsCodeValid(request);
            await this.CreateCode(request);

            return Unit.Value;
        }
    }
}
