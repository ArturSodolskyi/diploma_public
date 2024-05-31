using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.UserCompanies.Queries.Exists;
using Module.Explorer.Contracts.Competencies.Queries.GetCompetencies;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Explorer.Application.Competencies.Queries.GetCompetencies
{
    public class GetCompetenciesQueryAuthorizer : IAuthorizer<GetCompetenciesQuery>
    {
        private readonly IExplorerDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public GetCompetenciesQueryAuthorizer(IExplorerDbContext dbContext,
            IUserAccessor userAccessor,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task AuthorizeAsync(GetCompetenciesQuery request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return;
            }

            var entity = await _dbContext.Jobs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.JobId, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Job), request.JobId);
            }

            var exists = await _mediator.Send(new ExistsQuery
            {
                UserId = _userAccessor.UserId,
                CompanyId = entity.CompanyId
            }, cancellationToken);

            if (!exists)
            {
                throw new ForbiddenException();
            }
        }
    }
}
