using MediatR;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.Companies.Commands.Create;
using Module.Companies.Domain;
using Module.Companies.Persistence;
using Shared.Accessors;
using System.Transactions;

namespace Module.Companies.Application.Companies.Commands.Create
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, int>
    {
        private readonly ICompaniesDbContext _dbContext;
        private readonly IUserAccessor _userAccessor;
        public CreateCompanyCommandHandler(ICompaniesDbContext dbContext,
            IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }

        public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var item = new Company
            {
                UserId = _userAccessor.UserId,
                Name = request.Name
            };

            using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);

            await _dbContext.Companies.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var userCompany = new UserCompany
            {
                UserId = item.UserId,
                CompanyId = item.Id,
                RoleId = (int)CompanyRoleEnum.Administrator
            };

            await _dbContext.UserCompanies.AddAsync(userCompany, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            scope.Complete();

            return item.Id;
        }
    }
}
