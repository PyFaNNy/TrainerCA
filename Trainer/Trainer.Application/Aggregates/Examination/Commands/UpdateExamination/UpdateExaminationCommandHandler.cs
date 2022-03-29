using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scriban;
using Trainer.Application.Abstractions;
using Trainer.Application.Exceptions;
using Trainer.Application.Interfaces;
using Trainer.Application.Models.Email;
using Trainer.Application.Templates;
using Trainer.Settings.Error;

namespace Trainer.Application.Aggregates.Examination.Commands.UpdateExamination
{
    public class UpdateExaminationCommandHandler : AbstractRequestHandler, IRequestHandler<UpdateExaminationCommand, Unit>
    {
        private readonly IMailService EmailService;
        private readonly ExaminationErrorSettings ExaminationErrorSettings;


        public UpdateExaminationCommandHandler(
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

        public async Task<Unit> Handle(UpdateExaminationCommand request, CancellationToken cancellationToken)
        {
            if (ExaminationErrorSettings.UpdateExaminationEnable)
            {
                CountIndicators(request);

                var examination = await this.DbContext.Examinations
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

                if (examination == null)
                {
                    throw new NotFoundException(nameof(Domain.Entities.Examination.Examination), request.Id);
                }

                this.Mapper.Map(request, examination);

                if (examination.Indicators == 0)
                {
                    throw new ValidationException("Sensors","You must select at least one sensor");
                }

                await this.DbContext.SaveChangesAsync(cancellationToken);

                var patient = await DbContext.Patients
                    .Where(x => x.Id == examination.PatientId)
                    .FirstOrDefaultAsync(cancellationToken);

                var doctor = await DbContext.Doctors
                    .Where(x => x.Id == examination.PatientId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (ExaminationErrorSettings.UpdateEmailExaminationEnable)
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
                        Subject = $"Update Examination by {doctor?.FirstName}"
                    });
                }
            }
            return Unit.Value;
        }

        private void CountIndicators(UpdateExaminationCommand model)
        {
            model.Indicators = 0;
            if (model.Indicator1)
            {
                model.Indicators += 1;
            }
            if (model.Indicator2)
            {
                model.Indicators += 2;
            }
            if (model.Indicator3)
            {
                model.Indicators += 4;
            }
            if (model.Indicator4)
            {
                model.Indicators += 8;
            }
        }
    }
}
