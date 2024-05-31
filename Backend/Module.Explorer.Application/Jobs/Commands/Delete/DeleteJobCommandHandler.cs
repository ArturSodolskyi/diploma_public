using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Jobs.Commands.Delete;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;
using Shared.Exceptions;

namespace Module.Explorer.Application.Jobs.Commands.Delete
{
    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand>
    {
        private readonly IExplorerDbContext _dbContext;
        public DeleteJobCommandHandler(IExplorerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Jobs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Job), request.Id);
            }

            _dbContext.Jobs.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
