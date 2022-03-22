namespace Trainer.Application.Aggregates.OTPCodes
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Scriban;
    using System;
    using System.Threading.Tasks;
    using Trainer.Application.Abstractions;
    using Trainer.Application.Exceptions;
    using Trainer.Application.Interfaces;
    using Trainer.Application.Models.Email;
    using Trainer.Application.Templates;
    using Trainer.Common;
    using Trainer.Domain.Entities;
    using Trainer.Enums;

    public abstract class RequestSmsCodeAbstractCommandHandler : AbstractRequestHandler
    {
        protected IMailService EmailService
        {
            get;
        }

        public RequestSmsCodeAbstractCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IMailService emailService)
             : base(mediator, dbContext, mapper)
        {
            this.EmailService = emailService;
        }

        protected void LimitsCodeValid(RequestSmsCodeAbstractCommand request)
        {
            var otp = this.DbContext.OTPs
                .Where(x => x.Email.Equals(request.Email))
                .Where(x => x.Action == request.Action)
                .Where(x => x.CreatedAt.AddHours(3) > DateTime.UtcNow)
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            if (otp.Count >= 3)
            {
                throw new ValidationException(nameof(otp), "You've exceeded the limit of verification codes that can be sent. Please, try again in 3 hours");
            }
        }

        protected async Task CreateCode(RequestSmsCodeAbstractCommand request)
        {
            var code = CodeGenerator.GenerateCode();

            this.DbContext.OTPs.Add(new OTP
            {
                Action = request.Action,
                Value = code,
                IsValid = true,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow
            });

            this.DbContext.SaveChanges();

            var template = Template.Parse(EmailTemplates.CodeEmail);

            var body = template.Render(new
            {
                code = code,
                link = $"https://{request.Host}/OTP/VerifyCode?otpAction={request.Action}&email={request.Email}"
            }) ;

            await this.EmailService.SendEmailAsync(new MailRequest
            {
                ToEmail = request.Email,
                Body = body,
                Subject = "Code"
            });
        }

        protected async Task UserMustBeExisted(string email)
        {
            var isUserExist = await this.DbContext.BaseUsers.AnyAsync(x => x.Email == email);

            if (!isUserExist)
            {
                throw new ValidationException(nameof(BaseUser), "Wrond email");
            }
        }
    }
}
