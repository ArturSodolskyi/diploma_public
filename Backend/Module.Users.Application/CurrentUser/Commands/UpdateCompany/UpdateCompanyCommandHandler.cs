using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Users.Contracts.CurrentUser.Commands.UpdateCompany;
using Module.Users.Domain;
using Module.Users.Persistence;
using Shared.Accessors;
using Shared.Exceptions;

namespace Module.Users.Application.CurrentUser.Commands.UpdateCompany
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        public UpdateCompanyCommandHandler(IUsersDbContext dbContext,
            IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }

        public async Task Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == _userAccessor.UserId, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(User), _userAccessor.UserId);
            }

            entity.CompanyId = request.CompanyId;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
