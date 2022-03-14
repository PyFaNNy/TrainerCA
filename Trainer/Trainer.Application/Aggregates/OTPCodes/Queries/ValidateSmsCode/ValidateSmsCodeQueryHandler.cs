namespace Trainer.Application.Aggregates.OTPCodes.Queries.ValidateSmsCode
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Entities;
    using Trainer.Application.Interfaces;
    using Trainer.Settings;
    using MediatR;
    using Microsoft.Extensions.Options;
    using Trainer.Application.Exceptions;
    using Trainer.Application.Abstractions;

    public class ValidateSmsCodeQueryHandler
        : AbstractRequestHandler, IRequestHandler<ValidateSmsCodeQuery, Code>
    {
        private readonly TwilioSettings configuration;

        public ValidateSmsCodeQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IOptions<TwilioSettings> configuration)
            : base(mediator, dbContext, mapper)
        {
            this.configuration = configuration.Value;
        }

        public Task<Code> Handle(ValidateSmsCodeQuery request, CancellationToken cancellationToken)
        {
            var isPhoneExisted = this.DbContext.BaseUsers
                .Any(x => x.PhoneNumber.Equals(request.PhoneNumber));

            if (!isPhoneExisted)
            {
                throw new NotFoundException(nameof(BaseUser.PhoneNumber), request.PhoneNumber);
            }

            if (this.configuration.IsUniversalVerificationCodeEnabled && request.Code.Equals(this.configuration.UniversalVerificationCode))
            {
                return Task.FromResult(new Code
                {
                    CodeValue = request.Code,
                    IsValid = true
                });
            }

            var isValid = this.DbContext.OTPs
                .Where(x => x.PhoneNumber == request.PhoneNumber)
                .Where(x => x.Action == request.Action)
                .Where(x => x.CreatedAt > DateTime.UtcNow.AddHours(-1))
                .Any(x => x.Value == request.Code);

            return Task.FromResult(new Code
            {
                CodeValue = request.Code,
                IsValid = isValid
            });
        }

        private async Task<Unit> Register(ValidateSmsCodeQuery request)
        {
            var user = this.DbContext.BaseUsers.FirstOrDefault(x => x.PhoneNumber.Equals(request.PhoneNumber));

            if (user != null)
            {
                user.PhoneNumberConfirmed = true;
                this.DbContext.BaseUsers.Update(user);
                this.DbContext.SaveChanges();
            }
            else
            {
                throw new ValidationException(nameof(request.PhoneNumber), "Wrong Phone Number");
            }

            return Unit.Value;
        }

        private async Task<Unit> EmailConfirmation(ValidateSmsCodeQuery request)
        {
            var user = this.DbContext.BaseUsers.FirstOrDefault(x => x.PhoneNumber.Equals(request.PhoneNumber));

            if (user != null)
            {
                user.EmailConfirmed = true;
                this.DbContext.BaseUsers.Update(user);
                this.DbContext.SaveChanges();
            }
            else
            {
                throw new ValidationException(nameof(request.PhoneNumber), "Wrong Phone Number");
            }

            return Unit.Value;
        }
    }
}
