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

    public class RequestLoginCodeCommandHandler
        : RequestSmsCodeAbstractCommandHandler, IRequestHandler<RequestLoginCodeCommand, Unit>
    {
        public RequestLoginCodeCommandHandler(IMediator mediator, ITrainerDbContext dbContext, IMapper mapper, IMailService emailService)
            : base(mediator, dbContext, mapper, emailService)
        {
        }

        public async Task<Unit> Handle(RequestLoginCodeCommand request, CancellationToken cancellationToken)
        {
            this.CredentialsMustBeValid(request);
            this.LimitsCodeValid(request);
            await this.CreateCode(request);

            return Unit.Value;
        }

        private void CredentialsMustBeValid(RequestLoginCodeCommand request)
        {
            var user = this.DbContext.BaseUsers
                .Where(x => x.Email.Equals(request.Email))
                .FirstOrDefault();

            if (user == null || !CryptoHelper.VerifyHashedPassword(user.PasswordHash, request.Password))
            {
                throw new ValidationException(nameof(request.Password), "Wrong login/password");
            }
        }
    }
}
