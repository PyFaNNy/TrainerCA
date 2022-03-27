using AutoMapper;
using MediatR;
using Trainer.Application.Abstractions;
using Trainer.Application.Interfaces;
using Scriban;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Models.Email;
using Trainer.Application.Templates;
using Trainer.Settings.Error;
using Microsoft.Extensions.Options;

namespace Trainer.Application.Aggregates.Examination.Commands.CreateExamination
{
    public class CreateExaminationCommandHandler : AbstractRequestHandler, IRequestHandler<CreateExaminationCommand, Unit>
    {
        private readonly IMailService EmailService;
        private readonly ExaminationErrorSettings ExaminationErrorSettings;

        public CreateExaminationCommandHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper,
            IMailService mailService,
            IOptions<ExaminationErrorSettings> examinationErrorSettings)
            : base(mediator, dbContext, mapper)
        {
            EmailService = mailService;
            ExaminationErrorSettings = examinationErrorSettings.Value;
        }

        public async Task<Unit> Handle(CreateExaminationCommand request, CancellationToken cancellationToken)
        {
            if (ExaminationErrorSettings.CreateExaminationEnable)
            {
                var examination = this.Mapper.Map<Domain.Entities.Examination.Examination>(request);

                await this.DbContext.Examinations.AddAsync(examination, cancellationToken);
                await this.DbContext.SaveChangesAsync(cancellationToken);

                var patient = await DbContext.Patients
                    .Where(x => x.Id == examination.PatientId)
                    .FirstOrDefaultAsync(cancellationToken);

                var doctor = await DbContext.Doctors
                    .Where(x => x.Id == examination.PatientId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (ExaminationErrorSettings.CreateEmailExaminationEnable)
                {
                    var template = Template.Parse(EmailTemplates.ExaminationEmail);

                    var body = template.Render(new
                    {
                        patient = patient,
                        model = request
                    });

                    await EmailService.SendEmailAsync(new MailRequest
                    {
                        ToEmail = patient.Email,
                        Body = body,
                        Subject = $"Set Examination by {doctor?.FirstName}"
                    });
                }
            }
            return Unit.Value;
        }
    }
}
