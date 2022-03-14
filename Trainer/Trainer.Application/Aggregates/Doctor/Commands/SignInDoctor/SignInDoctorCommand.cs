using AutoMapper;
using MediatR;
using Trainer.Application.Mappings;

namespace Trainer.Application.Aggregates.Doctor.Commands.SignInDoctor
{
    public class SignInDoctorCommand : IRequest<Unit>, IMapTo<Domain.Entities.Doctor.Doctor>
    {
        public string Email
        {
            get;
            set;
        }

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

        public string Password
        {
            get;
            set;
        }

        public string ConfirmPassword
        {
            get;
            set;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SignInDoctorCommand, Domain.Entities.Doctor.Doctor>()
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => Enums.StatusUser.Active))
                .ForMember(d => d.UserRole, opt => opt.MapFrom(s => Enums.UserRole.Doctor));
        }
    }
}
