using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Users.Contracts.Users.Commands.ClearUserCompany;
using Module.Users.Domain;
using Module.Users.Persistence;
using Shared.Exceptions;

namespace Module.Users.Application.Users.Commands.ClearUserCompany
{
    public class ClearUserCompanyCommandHandler : IRequestHandler<ClearUserCompanyCommand>
    {
        private readonly IUsersDbContext _dbContext;
        public ClearUserCompanyCommandHandler(IUsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(ClearUserCompanyCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (user is null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            if (user.CompanyId != request.CompanyId)
            {
                return;
            }

            user.CompanyId = null;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
