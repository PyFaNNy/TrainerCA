namespace Trainer.Application.Aggregates.OTPCodes.Commands.RequestLoginCode
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Trainer.Application.Exceptions;
    using Trainer.Application.Interfaces;
    using Trainer.Common.TableConnect.Common;
    using MediatR;
    using Trainer.Settings.Error;
    using Microsoft.Extensions.Options;

    public class RequestLoginCodeCommandHandler
        : RequestSmsCodeAbstractCommandHandler, IRequestHandler<RequestLoginCodeCommand, Unit>
    {
        public RequestLoginCodeCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper, IMailService emailService, 
            IOptions<OTPCodesErrorSettings> otpCodesErrorSettings)
            : base(mediator, dbContext, mapper, emailService, otpCodesErrorSettings)
        {
        }

        public async Task<Unit> Handle(RequestLoginCodeCommand request, CancellationToken cancellationToken)
        {
            if (OTPCodesErrorSettings.RequestLoginCodeEnable)
            {
                this.CredentialsMustBeValid(request);
                this.LimitsCodeValid(request);
                await this.CreateCode(request);
            }
            return Unit.Value;
        }

        private void CredentialsMustBeValid(RequestLoginCodeCommand request)
        {
            var user = this.DbContext.BaseUsers
                .Where(x => x.Email.Equals(request.Email))
                .FirstOrDefault();

            if (user == null)
            {
                throw new ValidationException( "Email","Wrong email");
            }
        }
    }
}
