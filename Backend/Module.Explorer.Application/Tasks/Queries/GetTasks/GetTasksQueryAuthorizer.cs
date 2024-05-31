using MediatR;
using Module.Companies.Contracts.UserCompanies.Queries.Exists;
using Module.Explorer.Contracts.Competencies.Queries.GetCompanyId;
using Module.Explorer.Contracts.Tasks.Queries.GetTasks;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Explorer.Application.Tasks.Queries.GetTasks
{
    public class GetTasksQueryAuthorizer : IAuthorizer<GetTasksQuery>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public GetTasksQueryAuthorizer(IUserAccessor userAccessor,
            IMediator mediator)
        {
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task AuthorizeAsync(GetTasksQuery request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return;
            }

            var companyId = await _mediator.Send(new GetCompanyIdQuery
            {
                CompetenceId = request.CompetenceId
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
