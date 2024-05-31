using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.UserCompanies.Queries.Exists;
using Module.Companies.Persistence;

namespace Module.Companies.Application.UserCompanies.Queries.Exists
{
    public class ExistsQueryHandler : IRequestHandler<ExistsQuery, bool>
    {
        private readonly ICompaniesDbContext _dbContext;
        public ExistsQueryHandler(ICompaniesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(ExistsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.UserCompanies
                .AnyAsync(x => x.UserId == request.UserId && x.CompanyId == request.CompanyId, cancellationToken);
        }
    }
}
