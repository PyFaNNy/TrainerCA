namespace Prixy.Application.Aggregates.OTPCodes.Queries.GetSmsOtp;
using MediatR;
using Trainer.Enums;

public class GetSmsOtpQuery: IRequest<Trainer.Domain.Entities.OTP>
{
    public GetSmsOtpQuery(string email, string code, OTPAction action)
    {
        this.Email = email;
        this.Code = code;
        this.Action = action;
    }

    public string Email
    {
        get;
    }

    public string Code
    {
        get;
    }

    public OTPAction Action
    {
        get;
    }
}
