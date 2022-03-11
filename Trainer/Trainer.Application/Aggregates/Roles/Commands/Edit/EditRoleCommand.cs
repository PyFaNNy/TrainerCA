using AutoMapper;
using MediatR;
using Trainer.Application.Mappings;

namespace Trainer.Application.Aggregates.Roles.Commands.Edit
{
    public class EditRoleCommand : IRequest<Unit>, IMapTo<Domain.Entities.Role.Role>
    {
        public Guid RoleId 
        { 
            get;
            set;
        }

        public string RoleName
        {
            get;
            set;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EditRoleCommand, Domain.Entities.Role.Role>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.RoleName))
                .ForMember(d => d.NormalizedName, opt => opt.MapFrom(s => s.RoleName.ToUpper()));
        }
    }
}
