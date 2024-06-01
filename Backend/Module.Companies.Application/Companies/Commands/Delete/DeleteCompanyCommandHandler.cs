using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.Companies.Commands.Delete;
using Module.Companies.Domain;
using Module.Companies.Persistence;
using Module.Users.Contracts.Users.Commands.ClearCompany;
using Shared.Exceptions;
using System.Transactions;

namespace Module.Companies.Application.Companies.Commands.Delete
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly ICompaniesDbContext _dbContext;
        private readonly IMediator _mediator;
        public DeleteCompanyCommandHandler(ICompaniesDbContext dbContext,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Companies
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Company), request.Id);
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _mediator.Send(new ClearCompanyCommand
            {
                CompanyId = request.Id
            }, cancellationToken);

            var userCompanies = await _dbContext.UserCompanies
                .Where(x => x.CompanyId == request.Id)
                .ToArrayAsync(cancellationToken);
            _dbContext.UserCompanies.RemoveRange(userCompanies);

            var userCompanyInvitations = await _dbContext.UserCompanyInvitations
                .Where(x => x.CompanyId == request.Id)
                .ToArrayAsync(cancellationToken);
            _dbContext.UserCompanyInvitations.RemoveRange(userCompanyInvitations);

            _dbContext.Companies.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            scope.Complete();
        }
    }
}
