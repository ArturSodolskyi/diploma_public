using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.UserCompanies.Queries.Exists;
using Module.Explorer.Contracts.Competencies.Queries.GetCompanyId;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Explorer.Application.Competencies.Queries.GetCompanyId
{
    public class GetCompanyIdQueryAuthorizer : IAuthorizer<GetCompanyIdQuery>
    {
        private readonly IExplorerDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public GetCompanyIdQueryAuthorizer(IExplorerDbContext dbContext,
            IUserAccessor userAccessor,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task AuthorizeAsync(GetCompanyIdQuery request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return;
            }

            var competence = await _dbContext.Competencies
                .Include(x => x.Job)
                .FirstOrDefaultAsync(x => x.Id == request.CompetenceId, cancellationToken);
            if (competence is null)
            {
                throw new NotFoundException(nameof(Competence), request.CompetenceId);
            }

            var companyId = competence.Job.CompanyId;
            var exists = await _mediator.Send(new ExistsQuery
            {
                CompanyId = companyId,
                UserId = _userAccessor.UserId
            }, cancellationToken);

            if (!exists)
            {
                throw new ForbiddenException();
            }
        }
    }
}