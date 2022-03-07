using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;
using Trainer.Domain.Entities.User;

namespace Trainer.Application.Aggregates.Users.Commands.SignInUser
{
    public class SignInUserCommandHandler : AbstractRequestHandler, IRequestHandler<SignInUserCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;

        public SignInUserCommandHandler(
        IMediator mediator,
        ITrainerDbContext dbContext,
        IMapper mapper,
        UserManager<User> userManager
        )
        : base(mediator, dbContext, mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException($"{nameof(userManager)} is null.");
        }

        public async Task<IdentityResult> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var user = this.Mapper.Map<User>(request);

            var result = await _userManager.CreateAsync(user, request.Password);
            await _userManager.AddToRoleAsync(user, "unknown");

            return result;
        }
    }
}
