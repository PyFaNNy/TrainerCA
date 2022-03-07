using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Trainer.Application.Mappings;
using Trainer.Domain.Entities.User;

namespace Trainer.Application.Aggregates.Users.Commands.SignInUser
{
    public class SignInUserCommand : IRequest<IdentityResult>, IMapTo<User>
    {
        public string Email
        {
            get;
            set;
        }

        public string UserName
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
            profile.CreateMap<SignInUserCommand, User>()
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.MiddleName, opt => opt.MapFrom(s => s.MiddleName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.NormalizedEmail, opt => opt.MapFrom(s => s.Email.ToUpper()))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => Enums.StatusUser.Active))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserName))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserName.ToUpper()));
        }
    }
}
