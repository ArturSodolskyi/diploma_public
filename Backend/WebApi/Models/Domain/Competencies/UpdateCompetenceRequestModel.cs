using AutoMapper;
using Module.Explorer.Contracts.Competencies.Commands.Update;
using Shared.Mapping;

namespace WebApi.Models.Domain.Competencies
{
    public class UpdateCompetenceRequestModel : IMapWith<UpdateCompetenceCommand>
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCompetenceRequestModel, UpdateCompetenceCommand>()
                .ForMember(d => d.Id, m => m.MapFrom(x => x.Id))
                .ForMember(d => d.Name, m => m.MapFrom(x => x.Name));
        }
    }
}