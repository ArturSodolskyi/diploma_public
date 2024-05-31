using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Tasks.Commands.Update;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;
using Shared.Exceptions;

namespace Module.Explorer.Application.Tasks.Commands.Update
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly IExplorerDbContext _dbContext;
        public UpdateTaskCommandHandler(IExplorerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Tasks
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(CompetenceTask), request.Id);
            }

            entity.Name = request.Name;
            entity.Text = request.Text;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
