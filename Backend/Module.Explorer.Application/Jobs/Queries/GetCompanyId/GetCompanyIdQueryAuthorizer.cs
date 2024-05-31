using MediatR;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRole;
using Module.Explorer.Contracts.Jobs.Queries.GetCompanyId;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Explorer.Application.Jobs.Queries.GetCompanyId
{
    public class GetCompanyIdQueryAuthorizer : IAuthorizer<GetCompanyIdQuery>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public GetCompanyIdQueryAuthorizer(IUserAccessor userAccessor,
            IMediator mediator)
        {
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task AuthorizeAsync(GetCompanyIdQuery request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return;
            }

            var companyId = await _mediator.Send(new GetCompanyIdQuery
            {
                JobId = request.JobId
            }, cancellationToken);

            var isAdministrator = await _mediator.Send(new IsInCompanyRoleQuery
            {
                CompanyId = companyId,
                UserId = _userAccessor.UserId,
                Role = CompanyRoleEnum.Administrator
            }, cancellationToken);

            if (!isAdministrator)
            {
                throw new ForbiddenException();
            }
        }
    }
}
