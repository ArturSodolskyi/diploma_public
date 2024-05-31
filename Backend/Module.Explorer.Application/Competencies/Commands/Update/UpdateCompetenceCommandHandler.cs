using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Competencies.Commands.Update;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;
using Shared.Exceptions;

namespace Module.Explorer.Application.Competencies.Commands.Update
{
    public class UpdateCompetenceCommandHandler : IRequestHandler<UpdateCompetenceCommand>
    {
        private readonly IExplorerDbContext _dbContext;
        public UpdateCompetenceCommandHandler(IExplorerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateCompetenceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Competencies
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Competence), request.Id);
            }

            entity.Name = request.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
