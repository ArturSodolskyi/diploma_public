using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Tasks.Commands.Delete;
using Module.Explorer.Contracts.Tasks.Commands.DeleteByCompetence;
using Module.Explorer.Persistence;
using System.Transactions;

namespace Module.Explorer.Application.Tasks.Commands.DeleteByCompetence
{
    public class DeleteByCompetenceCommandHandler : IRequestHandler<DeleteByCompetenceCommand>
    {
        private readonly IExplorerDbContext _dbContext;
        private readonly IMediator _mediator;
        public DeleteByCompetenceCommandHandler(IExplorerDbContext dbContext,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task Handle(DeleteByCompetenceCommand request, CancellationToken cancellationToken)
        {
            var tasks = await _dbContext.Tasks
                .Where(x => x.CompetenceId == request.CompetenceId)
                .ToListAsync(cancellationToken);

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            foreach (var task in tasks)
            {
                await _mediator.Send(new DeleteTaskCommand
                {
                    Id = task.Id
                }, cancellationToken);
            }

            scope.Complete();
        }
    }
}
