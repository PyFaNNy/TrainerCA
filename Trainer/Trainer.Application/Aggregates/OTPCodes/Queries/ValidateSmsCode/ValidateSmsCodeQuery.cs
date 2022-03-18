﻿namespace Trainer.Application.Aggregates.OTPCodes.Queries.ValidateSmsCode
{
    using MediatR;
    using Newtonsoft.Json;
    using Trainer.Enums;

    public class ValidateSmsCodeQuery : IRequest<Code>
    {
        public ValidateSmsCodeQuery(string email, string code, OTPAction action)
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
}
