using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Competencies.Commands.Delete;
using Module.Explorer.Contracts.Tasks.Commands.DeleteByCompetence;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;
using Shared.Exceptions;
using System.Transactions;

namespace Module.Explorer.Application.Competencies.Commands.Delete
{
    public class DeleteCompetenceCommandHandler : IRequestHandler<DeleteCompetenceCommand>
    {
        private readonly IExplorerDbContext _dbContext;
        private readonly IMediator _mediator;
        public DeleteCompetenceCommandHandler(IExplorerDbContext dbContext,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task Handle(DeleteCompetenceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Competencies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Competence), request.Id);
            }

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _mediator.Send(new DeleteByCompetenceCommand
            {
                CompetenceId = entity.Id
            }, cancellationToken);

            _dbContext.Competencies.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            scope.Complete();
        }
    }
}
