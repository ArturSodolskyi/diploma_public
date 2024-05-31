using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Users.Contracts.Users.Commands.ClearCompany;
using Module.Users.Persistence;

namespace Module.Users.Application.Users.Commands.ClearCompany
{
    public class ClearCompanyCommandHandler : IRequestHandler<ClearCompanyCommand>
    {
        private readonly IUsersDbContext _dbContext;
        public ClearCompanyCommandHandler(IUsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(ClearCompanyCommand request, CancellationToken cancellationToken)
        {
            await _dbContext.Users
                .Where(x => x.CompanyId == request.CompanyId)
                .ForEachAsync(x => x.CompanyId = null, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
