using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRole;
using Module.Explorer.Contracts.Jobs.Commands.Delete;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Explorer.Application.Jobs.Commands.Delete
{
    public class DeleteJobCommandAuthorizer : IAuthorizer<DeleteJobCommand>
    {
        private readonly IExplorerDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public DeleteJobCommandAuthorizer(IExplorerDbContext dbContext,
            IUserAccessor userAccessor,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task AuthorizeAsync(DeleteJobCommand request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return;
            }

            var entity = await _dbContext.Jobs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Job), request.Id);
            }

            var isAdministrator = await _mediator.Send(new IsInCompanyRoleQuery
            {
                CompanyId = entity.CompanyId,
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