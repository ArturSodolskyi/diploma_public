using MediatR;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRole;
using Module.Explorer.Contracts.Categories.Commands.Create;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Explorer.Application.Categories.Commands.Create
{
    public class CreateCategoryCommandAuthorizer : IAuthorizer<CreateCategoryCommand>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public CreateCategoryCommandAuthorizer(IUserAccessor userAccessor,
            IMediator mediator)
        {
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task AuthorizeAsync(CreateCategoryCommand request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return;
            }

            var isAdministrator = await _mediator.Send(new IsInCompanyRoleQuery
            {
                CompanyId = request.CompanyId,
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
