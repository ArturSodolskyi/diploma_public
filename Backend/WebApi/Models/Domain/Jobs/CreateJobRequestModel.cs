using AutoMapper;
using Module.Explorer.Contracts.Jobs.Commands.Create;
using Shared.Mapping;

namespace WebApi.Models.Domain.Jobs
{
    public class CreateJobRequestModel : IMapWith<CreateJobCommand>
    {
        public int CompanyId { get; set; }
        public int? CategoryId { get; set; }
        public required string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateJobRequestModel, CreateJobCommand>()
                .ForMember(d => d.CompanyId, m => m.MapFrom(x => x.CompanyId))
                .ForMember(d => d.CategoryId, m => m.MapFrom(x => x.CategoryId))
                .ForMember(d => d.Name, m => m.MapFrom(x => x.Name));
        }
    }
}
