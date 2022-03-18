using AutoMapper;
using MediatR;
using Trainer.Application.Mappings;
using Trainer.Common.TableConnect.Common;

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
                .ForMember(d => d.MiddleName, opt => opt.MapFrom(s => s.MiddleName))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.PasswordHash, opt => opt.MapFrom(s => CryptoHelper.HashPassword(s.Password)))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => Enums.StatusUser.Pending))
                .ForMember(d => d.Role, opt => opt.MapFrom(s => Enums.UserRole.Doctor));
        }
    }
}
