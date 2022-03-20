namespace Trainer.Application.Aggregates.OTPCodes.Commands.RequestRegistrationCode
{
    using AutoMapper;
    using Interfaces;
    using MediatR;
    using Microsoft.Extensions.Options;
    using System.Threading;
    using System.Threading.Tasks;
    using Trainer.Settings.Error;

    public class RequestRegistrationCodeCommandHandler : RequestSmsCodeAbstractCommandHandler, IRequestHandler<RequestRegistrationCodeCommand, Unit>
    {
        private readonly OTPCodesErrorSettings OTPCodesErrorSettings;

        public RequestRegistrationCodeCommandHandler(IMediator mediator, ITrainerDbContext dbContext, IMapper mapper, IMailService emailService,
        IOptions<OTPCodesErrorSettings> otpCodesErrorSettings)
    : base(mediator, dbContext, mapper, emailService)
        {
            OTPCodesErrorSettings = otpCodesErrorSettings.Value;
        }

        public async Task<Unit> Handle(RequestRegistrationCodeCommand request, CancellationToken cancellationToken)
        {
            if (OTPCodesErrorSettings.RequestLoginCodeEnable)
            {
                this.LimitsCodeValid(request);
                await this.CreateCode(request);
            }
            return Unit.Value;
        }
    }
}
