using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.UserCompanies.Queries.Exists;
using Module.Companies.Contracts.UserCompanyInvitations.Commands.Create;
using Module.Companies.Domain;
using Module.Companies.Persistence;
using Module.Users.Contracts.Users.Queries.GetUserByEmail;

namespace Module.Companies.Application.UserCompanyInvitations.Commands.Create
{
    public class CreateUserCompanyInvitationCommandHandler : IRequestHandler<CreateUserCompanyInvitationCommand>
    {
        private readonly ICompaniesDbContext _dbContext;
        private readonly IMediator _mediator;
        public CreateUserCompanyInvitationCommandHandler(ICompaniesDbContext dbContext,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task Handle(CreateUserCompanyInvitationCommand request, CancellationToken cancellationToken)
        {
            if (await InvitationExistsAsync(request, cancellationToken)
                || await IsCompanyMemberAsync(request, cancellationToken))
            {
                return;
            }

            var item = new UserCompanyInvitation
            {
                Email = request.Email,
                CompanyId = request.CompanyId,
            };

            await _dbContext.UserCompanyInvitations.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task<bool> InvitationExistsAsync(CreateUserCompanyInvitationCommand request, CancellationToken cancellationToken)
        {
            var invitation = await _dbContext.UserCompanyInvitations
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == request.Email
                && x.CompanyId == request.CompanyId, cancellationToken);
            return invitation is not null;
        }

        private async Task<bool> IsCompanyMemberAsync(CreateUserCompanyInvitationCommand request, CancellationToken cancellationToken)
        {
            var user = _mediator.Send(new GetUserByEmailQuery
            {
                Email = request.Email
            }, cancellationToken);

            if (user is null)
            {
                return false;
            }

            return await _mediator.Send(new ExistsQuery
            {
                CompanyId = request.CompanyId,
                UserId = user.Id
            }, cancellationToken);
        }
    }
}
