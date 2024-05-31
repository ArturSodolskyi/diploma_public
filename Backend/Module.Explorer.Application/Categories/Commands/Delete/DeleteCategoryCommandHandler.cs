using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Categories.Commands.Delete;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;
using Shared.Exceptions;

namespace Module.Explorer.Application.Categories.Commands.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IExplorerDbContext _dbContext;
        public DeleteCategoryCommandHandler(IExplorerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            await RemoveChildrens(request.Id, cancellationToken);

            var jobs = await _dbContext.Jobs
                .Where(x => x.CategoryId == request.Id)
                .AsNoTracking()
                .ToArrayAsync(cancellationToken);
            _dbContext.Jobs.RemoveRange(jobs);

            _dbContext.Categories.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task RemoveChildrens(int id, CancellationToken cancellationToken)
        {
            var childrens = await _dbContext.Categories
                .Where(x => x.ParentId == id)
                .ToListAsync(cancellationToken);
            foreach (var children in childrens)
            {
                await RemoveChildrens(children.Id, cancellationToken);
                _dbContext.Categories.Remove(children);

                var jobs = await _dbContext.Jobs
                    .Where(x => x.CategoryId == children.Id)
                    .AsNoTracking()
                    .ToArrayAsync(cancellationToken);

                _dbContext.Jobs.RemoveRange(jobs);
            }
        }
    }
}
