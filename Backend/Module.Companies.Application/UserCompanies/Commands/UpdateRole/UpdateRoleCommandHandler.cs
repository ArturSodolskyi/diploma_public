using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.UserCompanies.Commands.UpdateRole;
using Module.Companies.Domain;
using Module.Companies.Persistence;
using Shared.Exceptions;

namespace Module.Companies.Application.UserCompanies.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
    {
        private readonly ICompaniesDbContext _dbContext;
        public UpdateRoleCommandHandler(ICompaniesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.UserCompanies
                .FirstOrDefaultAsync(x => x.UserId == request.UserId
                    && x.CompanyId == request.CompanyId, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(UserCompany), request.UserId);
            }

            entity.RoleId = (int)request.Role;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
