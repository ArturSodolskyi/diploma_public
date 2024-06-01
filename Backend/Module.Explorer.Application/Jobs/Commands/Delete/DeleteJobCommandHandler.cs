using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Jobs.Commands.Delete;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;
using Module.Reviews.Contracts.Reviews.Commands.DeleteByJob;
using Shared.Exceptions;
using System.Transactions;

namespace Module.Explorer.Application.Jobs.Commands.Delete
{
    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand>
    {
        private readonly IExplorerDbContext _dbContext;
        private readonly IMediator _mediator;
        public DeleteJobCommandHandler(IExplorerDbContext dbContext,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Jobs
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Job), request.Id);
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _mediator.Send(new DeleteByJobCommand
            {
                JobId = request.Id
            }, cancellationToken);

            _dbContext.Jobs.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            scope.Complete();
        }
    }
}
