using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Tasks.Commands.Delete;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;
using Module.Reviews.Contracts.ReviewTasks.Commands.DeleteByTask;
using Shared.Exceptions;
using System.Transactions;

namespace Module.Explorer.Application.Tasks.Commands.Delete
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly IExplorerDbContext _dbContext;
        private readonly IMediator _mediator;
        public DeleteTaskCommandHandler(IExplorerDbContext dbContext,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Tasks
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(CompetenceTask), request.Id);
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _mediator.Send(new DeleteByTaskCommand { TaskId = request.Id }, cancellationToken);

            _dbContext.Tasks.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            scope.Complete();
        }
    }
}
