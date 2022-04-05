namespace Trainer.Application.Aggregates.OTPCodes.Commands.RequestPassword
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using Exceptions;
    using Interfaces;
    using Trainer.Domain.Entities;
    using Trainer.Settings.Error;
    using Microsoft.Extensions.Options;

    public class RequestPasswordCommandHandler : RequestSmsCodeAbstractCommandHandler, IRequestHandler<RequestPasswordCommand, Unit>
    {
        public RequestPasswordCommandHandler(IMediator mediator, ITrainerDbContext dbContext, IMapper mapper, IMailService emailService,
        IOptions<OTPCodesErrorSettings> otpCodesErrorSettings)
            : base(mediator, dbContext, mapper, emailService, otpCodesErrorSettings)
        {
        }

        public async Task<Unit> Handle(RequestPasswordCommand request, CancellationToken cancellationToken)
        {
            if (OTPCodesErrorSettings.RequestPasswordEnable)
            {
                this.CheckIfUserExists(request.Email);
                this.LimitsCodeValid(request);
                await this.CreateCode(request);
            }

            return Unit.Value;
        }

        private void CheckIfUserExists(string email)
        {
            var isUserExist = this.DbContext.BaseUsers.Any(x => x.Email == email);

            if (!isUserExist)
            {
                throw new ValidationException("IncorrectEmail", "Incorrect email");
            }
        }
    }
}
