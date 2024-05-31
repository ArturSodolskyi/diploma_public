using MediatR;
using Module.Companies.Contracts.UserCompanies.Queries.Exists;
using Module.Reviews.Contracts.Reviews.Queries.GetCompanyId;
using Module.Reviews.Contracts.Reviews.Queries.GetCompetencies;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Reviews.Application.Reviews.Queries.GetCompetencies
{
    public class GetCompetenciesQueryAuthorizer : IAuthorizer<GetCompetenciesQuery>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public GetCompetenciesQueryAuthorizer(IUserAccessor userAccessor,
            IMediator mediator)
        {
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task AuthorizeAsync(GetCompetenciesQuery request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return;
            }

            var companyId = await _mediator.Send(new GetCompanyIdQuery
            {
                ReviewId = request.ReviewId
            }, cancellationToken);

            var exists = await _mediator.Send(new ExistsQuery
            {
                UserId = _userAccessor.UserId,
                CompanyId = companyId
            }, cancellationToken);

            if (!exists)
            {
                throw new ForbiddenException();
            }
        }
    }
}
