namespace Prixy.Application.Aggregates.OTPCodes.Queries.GetSmsOtp;

using Enums;
using MediatR;

public class GetSmsOtpQuery: IRequest<Domain.Entities.OTP>
{
    public GetSmsOtpQuery(string phoneNumber, string code, OTPAction action)
    {
        this.PhoneNumber = phoneNumber;
        this.Code = code;
        this.Action = action;
    }

    public string PhoneNumber
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
