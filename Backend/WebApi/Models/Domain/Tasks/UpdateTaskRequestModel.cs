using AutoMapper;
using Module.Explorer.Contracts.Tasks.Commands.Update;
using Shared.Mapping;

namespace WebApi.Models.Domain.Tasks
{
    public class UpdateTaskRequestModel : IMapWith<UpdateTaskCommand>
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Text { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateTaskRequestModel, UpdateTaskCommand>()
                .ForMember(d => d.Id, m => m.MapFrom(x => x.Id))
                .ForMember(d => d.Name, m => m.MapFrom(x => x.Name))
                .ForMember(d => d.Text, m => m.MapFrom(x => x.Text));
        }
    }
}
