using AutoMapper;
using MediatR;
using Trainer.Application.Mappings;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.Examination.Commands.UpdateExamination
{
    public class UpdateExaminationCommand : IRequest<Unit>, IMapTo<Domain.Entities.Examination.Examination>
    {
        public Guid DoctorId
        {
            get;
            set;
        }

        public Guid Id
        {
            get;
            set;
        }

        public bool Indicator1
        {
            get;
            set;
        }

        public bool Indicator2
        {
            get;
            set;
        }

        public bool Indicator3
        {
            get;
            set;
        }

        public bool Indicator4
        {
            get;
            set;
        }

        public int Indicators
        {
            get;
            set;
        }

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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateExaminationCommand, Domain.Entities.Examination.Examination>()
                .ForMember(d => d.TypePhysicalActive, opt => opt.MapFrom(s => s.TypePhysicalActive))
                .ForMember(d => d.Indicators, opt => opt.MapFrom(s => s.Indicators))
                .ForMember(d => d.Date, opt => opt.MapFrom(s => s.Date));
        }
    }
}
