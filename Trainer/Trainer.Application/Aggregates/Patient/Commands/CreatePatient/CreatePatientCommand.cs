using AutoMapper;
using MediatR;
using Trainer.Application.Mappings;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.Patient.Commands.CreatePatient
{
    public class CreatePatientCommand : IRequest<Unit>, IMapTo<Domain.Entities.Patient.Patient>
    {
        public string FirstName
        {
            get;
            set;
        }

        public string MiddleName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public int Age
        {
            get;
            set;
        }

        public Sex Sex
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePatientCommand, Domain.Entities.Patient.Patient>()
                .ForMember(d => d.Age, opt => opt.MapFrom(s => s.Age))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.MiddleName, opt => opt.MapFrom(s => s.MiddleName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email));
        }
    }
}
