using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Categories.Commands.Delete;
using Module.Explorer.Contracts.Jobs.Commands.Delete;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;
using Shared.Exceptions;
using System.Transactions;

namespace Module.Explorer.Application.Categories.Commands.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IExplorerDbContext _dbContext;
        private readonly IMediator _mediator;
        public DeleteCategoryCommandHandler(IExplorerDbContext dbContext,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await RemoveChildrensAsync(request.Id, cancellationToken);

            var jobs = await _dbContext.Jobs
                .Where(x => x.CategoryId == request.Id)
                .ToArrayAsync(cancellationToken);
            _dbContext.Jobs.RemoveRange(jobs);

            _dbContext.Categories.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            scope.Complete();
        }

        private async Task RemoveChildrensAsync(int id, CancellationToken cancellationToken)
        {
            var childrens = await _dbContext.Categories
                .Where(x => x.ParentId == id)
                .ToListAsync(cancellationToken);
            foreach (var children in childrens)
            {
                await RemoveChildrensAsync(children.Id, cancellationToken);
                await RemoveJobsAsync(children.Id, cancellationToken);
                _dbContext.Categories.Remove(children);
            }
        }

        private async Task RemoveJobsAsync(int id, CancellationToken cancellationToken)
        {
            var jobs = await _dbContext.Jobs
                .Where(x => x.CategoryId == id)
                .ToArrayAsync(cancellationToken);

            foreach (var job in jobs)
            {
                await _mediator.Send(new DeleteJobCommand
                {
                    Id = job.Id
                }, cancellationToken);
            }
        }
    }
}
