using AutoMapper;
using MediatR;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Aggregates.Doctor.Commands.SignInDoctor
{
    public class SignInDoctorCommandHandler : AbstractRequestHandler, IRequestHandler<SignInDoctorCommand, Unit>
    {
        public SignInDoctorCommandHandler(
        IMediator mediator,
        ITrainerDbContext dbContext,
        IMapper mapper)
        : base(mediator, dbContext, mapper)
        {
        }

        public Task<Unit> Handle(SignInDoctorCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
