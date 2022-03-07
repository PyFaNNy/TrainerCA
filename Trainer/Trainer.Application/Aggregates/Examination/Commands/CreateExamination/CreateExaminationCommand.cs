using AutoMapper;
using MediatR;
using Trainer.Application.Mappings;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.Examination.Commands.CreateExamination
{
    public class CreateExaminationCommand : IRequest<Unit>, IMapTo<Domain.Entities.Examination.Examination>
    {
        public TypePhysicalActive TypePhysicalActive
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public Guid PatientId
        {
            get;
            set;
        }

        public int Indicators
        {
            get;
            set;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateExaminationCommand, Domain.Entities.Examination.Examination>()
                .ForMember(d => d.Date, opt => opt.MapFrom(s => s.Date))
                .ForMember(d => d.PatientId, opt => opt.MapFrom(s => s.PatientId))
                .ForMember(d => d.TypePhysicalActive, opt => opt.MapFrom(s => s.TypePhysicalActive))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => ExaminationStatus.Active))
                .ForMember(d => d.Indicators, opt => opt.MapFrom(s => s.Indicators));
        }
    }
}
