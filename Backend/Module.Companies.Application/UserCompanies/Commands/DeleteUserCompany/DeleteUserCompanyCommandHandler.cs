using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.UserCompanies.Commands.DeleteUserCompany;
using Module.Companies.Persistence;
using Module.Users.Contracts.Users.Commands.ClearUserCompany;
using System.Transactions;

namespace Module.Companies.Application.UserCompanies.Commands.DeleteUserCompany
{
    public class DeleteUserCompanyCommandHandler : IRequestHandler<DeleteUserCompanyCommand>
    {
        private readonly ICompaniesDbContext _dbContext;
        private readonly IMediator _mediator;
        public DeleteUserCompanyCommandHandler(ICompaniesDbContext dbContext,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task Handle(DeleteUserCompanyCommand request, CancellationToken cancellationToken)
        {
            var userCompany = await _dbContext.UserCompanies
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == request.UserId
                    && x.CompanyId == request.CompanyId, cancellationToken);
            if (userCompany is null)
            {
                return;
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _mediator.Send(new ClearUserCompanyCommand
            {
                UserId = request.UserId,
                CompanyId = request.CompanyId
            }, cancellationToken);

            _dbContext.UserCompanies.Remove(userCompany);

            await _dbContext.SaveChangesAsync(cancellationToken);

            scope.Complete();
        }
    }
}
