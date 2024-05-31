using AutoMapper;
using Module.Explorer.Domain;
using Shared.Mapping;

namespace Module.Explorer.Contracts.Tree.Queries.Get
{
    public class CategoryViewModel : IMapWith<Category>
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public required string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryViewModel>()
                .ForMember(d => d.Id, m => m.MapFrom(x => x.Id))
                .ForMember(d => d.ParentId, m => m.MapFrom(x => x.ParentId))
                .ForMember(d => d.Name, m => m.MapFrom(x => x.Name));
        }
    }
}
