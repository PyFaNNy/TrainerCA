using AutoMapper;
using MediatR;
using Trainer.Application.Mappings;
using Trainer.Common.TableConnect.Common;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.BaseUser.Commands.SignIn
{
    public class SignInCommand : IRequest<Unit>, IMapTo<Domain.Entities.Doctor.Doctor>, IMapTo<Domain.Entities.Manager.Manager>
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

        public UserRole Role
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
            profile.CreateMap<SignInCommand, Domain.Entities.Doctor.Doctor>()
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.MiddleName, opt => opt.MapFrom(s => s.MiddleName))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.PasswordHash, opt => opt.MapFrom(s => CryptoHelper.HashPassword(s.Password)))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => Enums.StatusUser.WaitEmailConfirm))
                .ForMember(d => d.Role, opt => opt.MapFrom(s => s.Role));

            profile.CreateMap<SignInCommand, Domain.Entities.Manager.Manager>()
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.MiddleName, opt => opt.MapFrom(s => s.MiddleName))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.PasswordHash, opt => opt.MapFrom(s => CryptoHelper.HashPassword(s.Password)))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => Enums.StatusUser.WaitEmailConfirm))
                .ForMember(d => d.Role, opt => opt.MapFrom(s => s.Role));
        }
    }

}
