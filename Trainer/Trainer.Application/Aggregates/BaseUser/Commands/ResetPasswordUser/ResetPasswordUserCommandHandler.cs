using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;
using Trainer.Common.TableConnect.Common;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.BaseUser.Commands.ResetPasswordUser
{
    public class ResetPasswordUserCommandHandler : AbstractRequestHandler, IRequestHandler<ResetPasswordUserCommand, Unit>
    {
        private readonly BaseUserErrorSettings BaseUserErrorSettings;

        public ResetPasswordUserCommandHandler(
        IMediator mediator,
        ITrainerDbContext dbContext,
        IMapper mapper,
        IOptions<BaseUserErrorSettings> baseUserErrorSettings)
        : base(mediator, dbContext, mapper)
        {
            BaseUserErrorSettings = baseUserErrorSettings.Value;
        }

        public async Task<Unit> Handle(ResetPasswordUserCommand request, CancellationToken cancellationToken)
        {
            if(BaseUserErrorSettings.ResetPasswordUserEnable)
            {
                var user =await DbContext.BaseUsers
                .Where(x => x.Email == request.Email)
                .FirstOrDefaultAsync(cancellationToken);

                if (user == null)
                {
                    throw new NotFoundException(nameof(Domain.Entities.BaseUser), request.Email);
                }

                user.PasswordHash = CryptoHelper.HashPassword(request.Password);

                await DbContext.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}
