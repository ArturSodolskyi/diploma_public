using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Tasks.Commands.Delete;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;
using Shared.Exceptions;

namespace Module.Explorer.Application.Tasks.Commands.Delete
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly IExplorerDbContext _dbContext;
        public DeleteTaskCommandHandler(IExplorerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(CompetenceTask), request.Id);
            }

            _dbContext.Tasks.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
