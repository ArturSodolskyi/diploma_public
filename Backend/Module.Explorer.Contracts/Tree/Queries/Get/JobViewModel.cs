using AutoMapper;
using Module.Explorer.Domain;
using Shared.Mapping;

namespace Module.Explorer.Contracts.Tree.Queries.Get
{
    public class JobViewModel : IMapWith<Job>
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public required string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Job, JobViewModel>()
                .ForMember(d => d.Id, m => m.MapFrom(x => x.Id))
                .ForMember(d => d.CategoryId, m => m.MapFrom(x => x.CategoryId))
                .ForMember(d => d.Name, m => m.MapFrom(x => x.Name));
        }
    }
}
