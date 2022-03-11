using AutoMapper;
using MediatR;
using Trainer.Application.Mappings;

namespace Trainer.Application.Aggregates.Roles.Commands.Create
{
    public class CreateRoleCommand : IRequest<Unit>, IMapTo<Domain.Entities.Role.Role>
    {
        public string RoleName
        {
            get;
            set;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateRoleCommand, Domain.Entities.Role.Role>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.RoleName))
                .ForMember(d => d.NormalizedName, opt => opt.MapFrom(s => s.RoleName.ToUpper()))
                .ForMember(d => d.ConcurrencyStamp, opt => opt.MapFrom(s => Guid.NewGuid()));
        }
    }
}
