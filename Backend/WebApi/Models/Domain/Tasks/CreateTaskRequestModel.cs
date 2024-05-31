using AutoMapper;
using Module.Explorer.Contracts.Tasks.Commands.Create;
using Shared.Mapping;

namespace WebApi.Models.Domain.Tasks
{
    public class CreateTaskRequestModel : IMapWith<CreateTaskCommand>
    {
        public int CompetenceId { get; set; }
        public required string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTaskRequestModel, CreateTaskCommand>()
                .ForMember(d => d.CompetenceId, m => m.MapFrom(x => x.CompetenceId))
                .ForMember(d => d.Name, m => m.MapFrom(x => x.Name));
        }
    }
}
