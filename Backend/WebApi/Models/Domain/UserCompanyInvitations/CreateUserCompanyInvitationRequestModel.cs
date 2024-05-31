using AutoMapper;
using Module.Companies.Contracts.UserCompanyInvitations.Commands.Create;
using Shared.Mapping;

namespace WebApi.Models.Domain.UserCompanyInvitations
{
    public class CreateUserCompanyInvitationRequestModel : IMapWith<CreateUserCompanyInvitationCommand>
    {
        public required string Email { get; set; }
        public int CompanyId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserCompanyInvitationRequestModel, CreateUserCompanyInvitationCommand>()
                .ForMember(d => d.Email, m => m.MapFrom(x => x.Email))
                .ForMember(d => d.CompanyId, m => m.MapFrom(x => x.CompanyId));
        }
    }
}
