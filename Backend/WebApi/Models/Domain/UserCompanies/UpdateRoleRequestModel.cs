using AutoMapper;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.UserCompanies.Commands.UpdateRole;
using Shared.Mapping;

namespace WebApi.Models.Domain.UserCompanies
{
    public class UpdateRoleRequestModel : IMapWith<UpdateRoleCommand>
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public CompanyRoleEnum Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateRoleRequestModel, UpdateRoleCommand>()
                .ForMember(d => d.UserId, m => m.MapFrom(x => x.UserId))
                .ForMember(d => d.CompanyId, m => m.MapFrom(x => x.CompanyId))
                .ForMember(d => d.Role, m => m.MapFrom(x => x.Role));
        }
    }
}
