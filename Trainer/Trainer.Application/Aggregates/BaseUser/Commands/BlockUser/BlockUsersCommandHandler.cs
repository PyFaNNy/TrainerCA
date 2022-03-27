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

namespace Trainer.Application.Aggregates.BaseUser.Commands.BlockUser
{
    public class BlockUsersCommandHandler : AbstractRequestHandler, IRequestHandler<BlockUsersCommand, Unit>
    {
        private readonly IMailService EmailService;
        private readonly BaseUserErrorSettings BaseUserErrorSettings;

        public BlockUsersCommandHandler(
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

        public async Task<Unit> Handle(BlockUsersCommand request, CancellationToken cancellationToken)
        {
            if (BaseUserErrorSettings.BlockUserEnable)
            {
                foreach (var id in request.UserIds)
                {
                    var user = await this.DbContext.BaseUsers
                        .Where(x => x.Id == id)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (user == null)
                    {
                        throw new NotFoundException(nameof(Domain.Entities.BaseUser), id);
                    }

                    user.Status = Enums.StatusUser.Block;
                    if (BaseUserErrorSettings.BlockUserEmailEnable)
                    {
                        var template = Template.Parse(EmailTemplates.BlockUser);

                        var body = template.Render();

                        await EmailService.SendEmailAsync(new MailRequest
                        {
                            ToEmail = user.Email,
                            Body = body,
                            Subject = $"Block your account"
                        });
                    }
                }
                await this.DbContext.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
