using MediatR;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRoleByJob;
using Module.Explorer.Contracts.Competencies.Commands.Update;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Explorer.Application.Competencies.Commands.Update
{
    public class UpdateCompetenceCommandAuthorizer : IAuthorizer<UpdateCompetenceCommand>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public UpdateCompetenceCommandAuthorizer(IUserAccessor userAccessor,
            IMediator mediator)
        {
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task AuthorizeAsync(UpdateCompetenceCommand request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return;
            }

            var isAdministrator = await _mediator.Send(new IsInCompanyRoleByJobQuery
            {
                JobId = request.Id,
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