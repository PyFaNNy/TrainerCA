using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;
using Trainer.Domain.Entities.User;

namespace Trainer.Application.Aggregates.Users.Commands.BlockUser
{
    public class BlockUsersCommandHandler : AbstractRequestHandler, IRequestHandler<BlockUsersCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public BlockUsersCommandHandler(
        IMediator mediator,
        ITrainerDbContext dbContext,
        IMapper mapper)
        : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(BlockUsersCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.UserIds)
            {
                var user = await this.DbContext.Users
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync(cancellationToken);

                if (user == null)
                {
                    throw new NotFoundException(nameof(Domain.Entities.User.User), id);
                }

                user.Status = Enums.StatusUser.Block;
            }
            await this.DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
