using MediatR;
using Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRole;
using Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRoleByCompetence;
using Module.Explorer.Contracts.Competencies.Queries.GetCompanyId;

namespace Module.Companies.Application.UserCompanies.Queries.IsInCompanyRoleByCompetence
{
    public class IsInCompanyRoleByCompetenceQueryHandler : IRequestHandler<IsInCompanyRoleByCompetenceQuery, bool>
    {
        private readonly IMediator _mediator;
        public IsInCompanyRoleByCompetenceQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(IsInCompanyRoleByCompetenceQuery request, CancellationToken cancellationToken)
        {
            var companyId = await _mediator.Send(new GetCompanyIdQuery
            {
                CompetenceId = request.CompetenceId
            }, cancellationToken);

            return await _mediator.Send(new IsInCompanyRoleQuery
            {
                CompanyId = companyId,
                UserId = request.UserId,
                Role = request.Role
            }, cancellationToken);
        }
    }
}