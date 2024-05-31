using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Companies.Contracts.Companies.Commands.Update;
using Module.Companies.Domain;
using Module.Companies.Persistence;
using Shared.Exceptions;

namespace Module.Companies.Application.Companies.Commands.Update
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
    {
        private readonly ICompaniesDbContext _dbContext;
        public UpdateCompanyCommandHandler(ICompaniesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Companies
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Company), request.Id);
            }

            entity.Name = request.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}