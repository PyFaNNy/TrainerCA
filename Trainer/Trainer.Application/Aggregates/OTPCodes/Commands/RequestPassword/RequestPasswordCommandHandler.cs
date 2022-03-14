namespace Trainer.Application.Aggregates.OTPCodes.Commands.RequestPassword
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using Exceptions;
    using Interfaces;
    using Trainer.Domain.Entities;

    public class RequestPasswordCommandHandler : RequestSmsCodeAbstractCommandHandler, IRequestHandler<RequestPasswordCommand, Unit>
    {
        public RequestPasswordCommandHandler(IMediator mediator, ITrainerDbContext dbContext, IMapper mapper, IMailService emailService)
            : base(mediator, dbContext, mapper, emailService)
        {
        }

        public async Task<Unit> Handle(RequestPasswordCommand request, CancellationToken cancellationToken)
        {
            this.CheckIfUserExists(request.Email);
            this.LimitsCodeValid(request);
            await this.CreateCode(request);


            return Unit.Value;
        }

        private void CheckIfUserExists(string email)
        {
            var isUserExist = this.DbContext.BaseUsers.Any(x => x.Email == email);

            if (!isUserExist)
            {
                throw new ValidationException(nameof(BaseUser), "Wrong phone number/surname");
            }
        }
    }
}
