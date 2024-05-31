using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Competencies.Commands.Delete;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;
using Shared.Exceptions;

namespace Module.Explorer.Application.Competencies.Commands.Delete
{
    public class DeleteCompetenceCommandHandler : IRequestHandler<DeleteCompetenceCommand>
    {
        private readonly IExplorerDbContext _dbContext;
        public DeleteCompetenceCommandHandler(IExplorerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteCompetenceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Competencies
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Competence), request.Id);
            }

            _dbContext.Competencies.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
