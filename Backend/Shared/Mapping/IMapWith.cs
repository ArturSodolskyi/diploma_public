using AutoMapper;

namespace Shared.Mapping
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}
