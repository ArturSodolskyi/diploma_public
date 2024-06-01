using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.UserCompanyInvitations.Commands.RespondInvitation;
using Module.Companies.Domain;
using Module.Companies.Persistence;
using Shared.Accessors;
using Shared.Exceptions;

namespace Module.Companies.Application.UserCompanyInvitations.Commands.RespondInvitation
{
    public class RespondInvitationCommandHandler : IRequestHandler<RespondInvitationCommand>
    {
        private readonly ICompaniesDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        public RespondInvitationCommandHandler(ICompaniesDbContext dbContext,
            IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }

        public async Task Handle(RespondInvitationCommand request, CancellationToken cancellationToken)
        {
            var invitation = await _dbContext.UserCompanyInvitations
                .FirstOrDefaultAsync(x => x.Email == _userAccessor.Email
                    && x.CompanyId == request.CompanyId, cancellationToken);
            if (invitation is null)
            {
                throw new NotFoundException(nameof(UserCompanyInvitation), request.CompanyId);
            }

            if (request.Accept)
            {
                var userCompany = new UserCompany
                {
                    UserId = _userAccessor.UserId,
                    CompanyId = request.CompanyId,
                    RoleId = (int)CompanyRoleEnum.Watcher
                };
                await _dbContext.UserCompanies.AddAsync(userCompany, cancellationToken);
            }

            _dbContext.UserCompanyInvitations.Remove(invitation);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
