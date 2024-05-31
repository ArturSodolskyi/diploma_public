using MediatR;
using Module.Companies.Contracts.UserCompanyInvitations.Queries.GetCompanyInvitations;

namespace Module.Companies.Application.UserCompanyInvitations.Queries.GetCompanyInvitations
{
    public class GetCompanyInvitationsQuery : IRequest<List<CompanyInvitationViewModel>>
    {

    }
}
