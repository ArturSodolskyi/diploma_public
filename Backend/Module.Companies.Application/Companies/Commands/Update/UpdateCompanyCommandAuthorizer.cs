using MediatR;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.Companies.Commands.Update;
using Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRole;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Companies.Application.Companies.Commands.Update
{
    public class UpdateCompanyCommandAuthorizer : IAuthorizer<UpdateCompanyCommand>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public UpdateCompanyCommandAuthorizer(IUserAccessor userAccessor,
            IMediator mediator)
        {
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task AuthorizeAsync(UpdateCompanyCommand request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return;
            }

            var isAdministrator = await _mediator.Send(new IsInCompanyRoleQuery
            {
                CompanyId = request.Id,
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