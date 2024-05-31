using AutoMapper;
using Module.Companies.Contracts.Companies.Commands.Update;
using Shared.Mapping;

namespace WebApi.Models.Domain.Companies
{
    public class UpdateCompanyRequestModel : IMapWith<UpdateCompanyCommand>
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCompanyRequestModel, UpdateCompanyCommand>()
                .ForMember(d => d.Id, m => m.MapFrom(x => x.Id))
                .ForMember(d => d.Name, m => m.MapFrom(x => x.Name));
        }
    }
}
