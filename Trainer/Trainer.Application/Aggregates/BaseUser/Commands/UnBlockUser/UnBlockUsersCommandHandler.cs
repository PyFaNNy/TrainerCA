using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scriban;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;
using Trainer.Application.Models.Email;
using Trainer.Application.Templates;

namespace Trainer.Application.Aggregates.BaseUser.Commands.UnBlockUser
{
    public class UnBlockUsersCommandHandler : AbstractRequestHandler, IRequestHandler<UnBlockUsersCommand, Unit>
    {
        private readonly IMailService EmailService;

        public UnBlockUsersCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IMailService mailService)
            : base(mediator, dbContext, mapper)
        {
            EmailService = mailService;
        }

        public async Task<Unit> Handle(UnBlockUsersCommand request, CancellationToken cancellationToken)
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

                user.Status = Enums.StatusUser.Active;

                var template = Template.Parse(EmailTemplates.UnBlockUser);

                var body = template.Render();

                await EmailService.SendEmailAsync(new MailRequest
                {
                    ToEmail = user.Email,
                    Body = body,
                    Subject = $"Unblock your account"
                });
            }
            await this.DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
