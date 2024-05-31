using AutoMapper;
using Module.Explorer.Domain;
using Shared.Mapping;

namespace Module.Explorer.Contracts.Tasks.Queries.GetTasks
{
    public class TaskViewModel : IMapWith<CompetenceTask>
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Text { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CompetenceTask, TaskViewModel>()
                .ForMember(d => d.Id, m => m.MapFrom(x => x.Id))
                .ForMember(d => d.Name, m => m.MapFrom(x => x.Name))
                .ForMember(d => d.Text, m => m.MapFrom(x => x.Text));
        }
    }
}