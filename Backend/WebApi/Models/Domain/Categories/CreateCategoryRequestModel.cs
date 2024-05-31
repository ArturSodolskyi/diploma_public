using AutoMapper;
using Module.Explorer.Contracts.Categories.Commands.Create;
using Shared.Mapping;

namespace WebApi.Models.Domain.Categories
{
    public class CreateCategoryRequestModel : IMapWith<CreateCategoryCommand>
    {
        public int CompanyId { get; set; }
        public int? ParentId { get; set; }
        public required string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCategoryRequestModel, CreateCategoryCommand>()
                .ForMember(d => d.CompanyId, m => m.MapFrom(x => x.CompanyId))
                .ForMember(d => d.ParentId, m => m.MapFrom(x => x.ParentId))
                .ForMember(d => d.Name, m => m.MapFrom(x => x.Name));
        }
    }
}
