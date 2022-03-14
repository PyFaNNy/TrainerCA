namespace Prixy.Application.Aggregates.OTPCodes.Queries.GetSmsOtp;

using Abstractions;
using AutoMapper;
using Domain.Entities;
using Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Settings;

public class GetSmsOtpQueryHandler
: AbstractRequestHandler<GetSmsOtpQuery, Domain.Entities.OTP>
{
    private TwilioSettings TwilioSettings
    {
        get;
    }

    public GetSmsOtpQueryHandler(
        IMediator mediator,
        IPrixyDbContext dbContext,
        IMapper mapper,
        IOptions<TwilioSettings> twilioSettings) : base(mediator, dbContext, mapper)
    {
        this.TwilioSettings = twilioSettings.Value;
    }

    public override async Task<OTP> Handle(GetSmsOtpQuery request, CancellationToken cancellationToken)
    {
        if (this.TwilioSettings.IsUniversalVerificationCodeEnabled &&
            this.TwilioSettings.UniversalVerificationCode.Equals(request.Code))
        {
            return new OTP { Action = request.Action, Value = request.Code, IsValid = true, };
        }

        return await this.DbContext.OTPs
            .Where(x => x.PhoneNumber == request.PhoneNumber)
            .Where(x => x.Action == request.Action)
            .Where(x => x.CreatedAt > DateTime.UtcNow.AddHours(-1))
            .FirstOrDefaultAsync(x => x.Value == request.Code, cancellationToken);
    }
}
