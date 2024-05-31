using AutoMapper;
using Module.Explorer.Domain;
using Shared.Mapping;

namespace Module.Explorer.Contracts.Competencies.Queries.GetCompetencies
{
    public class CompetenceViewModel : IMapWith<Competence>
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Competence, CompetenceViewModel>()
                .ForMember(d => d.Id, m => m.MapFrom(x => x.Id))
                .ForMember(d => d.Name, m => m.MapFrom(x => x.Name));
        }
    }
}