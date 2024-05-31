using AutoMapper;
using Module.Companies.Contracts.UserCompanyInvitations.Commands.RespondInvitation;
using Shared.Mapping;

namespace WebApi.Models.Dashboard.Companies
{
    public class RespondInvitationRequestModel : IMapWith<RespondInvitationCommand>
    {
        public int CompanyId { get; set; }
        public bool Accept { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RespondInvitationRequestModel, RespondInvitationCommand>()
                .ForMember(d => d.CompanyId, m => m.MapFrom(x => x.CompanyId))
                .ForMember(d => d.Accept, m => m.MapFrom(x => x.Accept));
        }
    }
}
