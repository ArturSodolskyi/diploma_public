using MediatR;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRole;
using Module.Explorer.Contracts.Jobs.Queries.GetByIds;
using Module.Explorer.Contracts.Jobs.Queries.GetCompanyId;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Explorer.Application.Jobs.Queries.GetByIds
{
    public class GetByIdsQueryAuthorizer : IAuthorizer<GetByIdsQuery>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public GetByIdsQueryAuthorizer(IUserAccessor userAccessor,
            IMediator mediator)
        {
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task AuthorizeAsync(GetByIdsQuery request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return;
            }

            foreach (var jobId in request.Ids)
            {
                var companyId = await _mediator.Send(new GetCompanyIdQuery
                {
                    JobId = jobId
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
}
