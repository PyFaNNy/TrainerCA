using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scriban;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;
using Trainer.Application.Models.Email;
using Trainer.Application.Templates;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.BaseUser.Commands.ApproveUser
{
    public class ApproveUserCommandHandler : AbstractRequestHandler, IRequestHandler<ApproveUserCommand, Unit>
    {
        private readonly IMailService EmailService;
        private readonly BaseUserErrorSettings BaseUserErrorSettings;

        public ApproveUserCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IMailService mailService,
            IOptions<BaseUserErrorSettings> вaseUserErrorSettings)
            : base(mediator, dbContext, mapper)
        {
            EmailService = mailService;
            BaseUserErrorSettings = вaseUserErrorSettings.Value;
        }

        public async Task<Unit> Handle(ApproveUserCommand request, CancellationToken cancellationToken)
        {
            if (BaseUserErrorSettings.ApproveUserEnable)
            {
                var user = await this.DbContext.BaseUsers
                .Where(x => x.Id == request.UserId)
                .FirstOrDefaultAsync(cancellationToken);

                if (user == null)
                {
                    throw new NotFoundException(nameof(Domain.Entities.BaseUser), request.UserId);
                }

                user.Status = Enums.StatusUser.Active;
                await this.DbContext.SaveChangesAsync(cancellationToken);
                if (BaseUserErrorSettings.ApproveUserEmailEnable)
                {
                    var template = Template.Parse(EmailTemplates.ApproveUser);

                    var body = template.Render();

                    await EmailService.SendEmailAsync(new MailRequest
                    {
                        ToEmail = user.Email,
                        Body = body,
                        Subject = $"Approve registration"
                    });
                }
            }
            return Unit.Value;
        }
    }
}
