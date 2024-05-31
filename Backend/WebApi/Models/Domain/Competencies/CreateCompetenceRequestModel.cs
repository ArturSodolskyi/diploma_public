using AutoMapper;
using Module.Explorer.Contracts.Competencies.Commands.Create;
using Shared.Mapping;

namespace WebApi.Models.Domain.Competencies
{
    public class CreateCompetenceRequestModel : IMapWith<CreateCompetenceCommand>
    {
        public int JobId { get; set; }
        public required string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCompetenceRequestModel, CreateCompetenceCommand>()
                .ForMember(d => d.JobId, m => m.MapFrom(x => x.JobId))
                .ForMember(d => d.Name, m => m.MapFrom(x => x.Name));
        }
    }
}
