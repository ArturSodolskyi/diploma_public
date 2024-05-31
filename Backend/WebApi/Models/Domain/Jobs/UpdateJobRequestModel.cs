using AutoMapper;
using Module.Explorer.Contracts.Jobs.Commands.Update;
using Shared.Mapping;

namespace WebApi.Models.Domain.Jobs
{
    public class UpdateJobRequestModel : IMapWith<UpdateJobCommand>
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateJobRequestModel, UpdateJobCommand>()
                .ForMember(d => d.Id, m => m.MapFrom(x => x.Id))
                .ForMember(d => d.Name, m => m.MapFrom(x => x.Name));
        }
    }
}
