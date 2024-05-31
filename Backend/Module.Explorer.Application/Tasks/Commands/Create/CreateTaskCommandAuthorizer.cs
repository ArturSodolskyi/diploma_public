using MediatR;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRole;
using Module.Explorer.Contracts.Competencies.Queries.GetCompanyId;
using Module.Explorer.Contracts.Tasks.Commands.Create;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Explorer.Application.Tasks.Commands.Create
{
    public class CreateTaskCommandAuthorizer : IAuthorizer<CreateTaskCommand>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public CreateTaskCommandAuthorizer(IUserAccessor userAccessor,
            IMediator mediator)
        {
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task AuthorizeAsync(CreateTaskCommand request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return;
            }

            var companyId = await _mediator.Send(new GetCompanyIdQuery
            {
                CompetenceId = request.CompetenceId
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
