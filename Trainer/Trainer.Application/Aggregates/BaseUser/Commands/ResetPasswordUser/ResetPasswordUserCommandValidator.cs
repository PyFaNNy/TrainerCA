﻿using FluentValidation;
using Trainer.Common;

namespace Trainer.Application.Aggregates.BaseUser.Commands.ResetPasswordUser
{
    public class ResetPasswordUserCommandValidator : AbstractValidator<ResetPasswordUserCommand>
    {
        public ResetPasswordUserCommandValidator()
        {
            RuleFor(x => x.Password)
                .Must(PasswordsHelper.IsMeetsRequirements);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.Password);
        }
    }
}
