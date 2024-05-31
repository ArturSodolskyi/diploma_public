using AutoMapper;
using Module.Companies.Contracts.Companies.Commands.Create;
using Shared.Mapping;

namespace WebApi.Models.Domain.Companies
{
    public class CreateCompanyRequestModel : IMapWith<CreateCompanyCommand>
    {
        public required string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCompanyRequestModel, CreateCompanyCommand>()
                .ForMember(d => d.Name, m => m.MapFrom(x => x.Name));
        }
    }
}
