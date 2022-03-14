using AutoMapper;
using MediatR;
using Trainer.Application.Mappings;

namespace Trainer.Application.Aggregates.Manager.Commands.SignInManager
{
    public class SignInManagerCommand : IRequest<Unit>, IMapTo<Domain.Entities.Manager.Manager>
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
            profile.CreateMap<SignInManagerCommand, Domain.Entities.Manager.Manager>()
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => Enums.StatusUser.Active))
                .ForMember(d => d.UserRole, opt => opt.MapFrom(s => Enums.UserRole.Doctor));
        }
    }
}
