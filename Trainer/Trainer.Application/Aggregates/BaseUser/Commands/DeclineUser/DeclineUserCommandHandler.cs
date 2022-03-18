using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scriban;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;
using Trainer.Application.Models.Email;
using Trainer.Application.Templates;

namespace Trainer.Application.Aggregates.BaseUser.Commands.DeclineUser
{
    public class DeclineUserCommandHandler : AbstractRequestHandler, IRequestHandler<DeclineUserCommand, Unit>
    {
        private readonly IMailService EmailService;

        public DeclineUserCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IMailService mailService)
            : base(mediator, dbContext, mapper)
        {
            EmailService = mailService;
        }

        public async Task<Unit> Handle(DeclineUserCommand request, CancellationToken cancellationToken)
        {
            var user = await this.DbContext.BaseUsers
                .Where(x => x.Id == request.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.BaseUser), request.UserId);
            }

            user.Status = Enums.StatusUser.Decline;
            await this.DbContext.SaveChangesAsync(cancellationToken);

            var template = Template.Parse(EmailTemplates.DeclineUser);

            var body = template.Render();

            await EmailService.SendEmailAsync(new MailRequest
            {
                ToEmail = user.Email,
                Body = body,
                Subject = $"Decline registration"
            });

            return Unit.Value;
        }
    }
}
