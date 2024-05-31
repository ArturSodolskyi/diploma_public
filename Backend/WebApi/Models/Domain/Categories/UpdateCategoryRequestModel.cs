using AutoMapper;
using Module.Explorer.Contracts.Categories.Commands.Update;
using Shared.Mapping;

namespace WebApi.Models.Domain.Categories
{
    public class UpdateCategoryRequestModel : IMapWith<UpdateCategoryCommand>
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCategoryRequestModel, UpdateCategoryCommand>()
                .ForMember(d => d.Id, m => m.MapFrom(x => x.Id))
                .ForMember(d => d.Name, m => m.MapFrom(x => x.Name));
        }
    }
}
