using MediatR;
using Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRole;
using Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRoleByJob;
using Module.Explorer.Contracts.Jobs.Queries.GetCompanyId;

namespace Module.Companies.Application.UserCompanies.Queries.IsInCompanyRoleByJob
{
    public class IsInCompanyRoleByJobQueryHandler : IRequestHandler<IsInCompanyRoleByJobQuery, bool>
    {
        private readonly IMediator _mediator;
        public IsInCompanyRoleByJobQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(IsInCompanyRoleByJobQuery request, CancellationToken cancellationToken)
        {
            var companyId = await _mediator.Send(new GetCompanyIdQuery
            {
                JobId = request.JobId
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
