namespace Trainer.Application.Mappings
{
    using AutoMapper;

    public interface IMapTo<T>
    {
        public void Mapping(Profile profile) => profile.CreateMap(this.GetType(), typeof(T));
    }
}
