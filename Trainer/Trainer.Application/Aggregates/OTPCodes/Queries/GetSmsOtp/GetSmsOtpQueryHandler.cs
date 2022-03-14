namespace Prixy.Application.Aggregates.OTPCodes.Queries.GetSmsOtp;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;
using Trainer.Domain.Entities;

public class GetSmsOtpQueryHandler : AbstractRequestHandler, IRequestHandler<GetSmsOtpQuery, OTP>
{
    public GetSmsOtpQueryHandler(
        IMediator mediator,
        ITrainerDbContext dbContext,
        IMapper mapper) : base(mediator, dbContext, mapper)
    {
    }

    public async Task<OTP> Handle(GetSmsOtpQuery request, CancellationToken cancellationToken)
    {
        return await this.DbContext.OTPs
            .Where(x => x.Email == request.Email)
            .Where(x => x.Action == request.Action)
            .Where(x => x.CreatedAt > DateTime.UtcNow.AddHours(-1))
            .FirstOrDefaultAsync(x => x.Value == request.Code, cancellationToken);
    }
}
