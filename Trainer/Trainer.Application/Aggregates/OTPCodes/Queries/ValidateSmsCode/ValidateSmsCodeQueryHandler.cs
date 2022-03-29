using Microsoft.EntityFrameworkCore;

namespace Trainer.Application.Aggregates.OTPCodes.Queries.ValidateSmsCode
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Entities;
    using Trainer.Application.Interfaces;
    using MediatR;
    using Trainer.Application.Exceptions;
    using Trainer.Application.Abstractions;

    public class ValidateSmsCodeQueryHandler
        : AbstractRequestHandler, IRequestHandler<ValidateSmsCodeQuery, Code>
    {
        public ValidateSmsCodeQueryHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
            : base(mediator, dbContext, mapper)
        {
        }

        public async Task<Code> Handle(ValidateSmsCodeQuery request, CancellationToken cancellationToken)
        {
            var isEmailExisted = this.DbContext.BaseUsers
                .Any(x => x.Email.Equals(request.Email));

            if (!isEmailExisted)
            {
                throw new NotFoundException(nameof(BaseUser.Email), request.Email);
            }

            var code = await this.DbContext.OTPs
                .Where(x => x.Email == request.Email)
                .Where(x => x.Action == request.Action)
                .Where(x => x.CreatedAt > DateTime.UtcNow.AddMinutes(-5))
                .Where(x => x.Value == request.Code)
                .FirstOrDefaultAsync(cancellationToken);

            var isValid = code.IsValid;

            code.IsValid = false;

            await DbContext.SaveChangesAsync(cancellationToken);

            return new Code
            {
                CodeValue = request.Code,
                IsValid = isValid
            };
        }
    }
}
