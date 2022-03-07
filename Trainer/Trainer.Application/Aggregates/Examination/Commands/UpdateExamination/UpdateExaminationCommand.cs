using AutoMapper;
using MediatR;
using Trainer.Application.Mappings;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.Examination.Commands.UpdateExamination
{
    public class UpdateExaminationCommand : IRequest<Unit>, IMapTo<Domain.Entities.Examination.Examination>
    {
        public Guid ExaminationId
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateExaminationCommand, Domain.Entities.Examination.Examination>()
                .ForMember(d => d.TypePhysicalActive, opt => opt.MapFrom(s => s.TypePhysicalActive))
                .ForMember(d => d.Indicators, opt => opt.MapFrom(s => s.Indicators));
        }
    }
}
