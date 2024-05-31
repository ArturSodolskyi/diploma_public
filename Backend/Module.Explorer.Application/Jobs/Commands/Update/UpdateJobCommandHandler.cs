using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Jobs.Commands.Update;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;
using Shared.Exceptions;

namespace Module.Explorer.Application.Jobs.Commands.Update
{
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand>
    {
        private readonly IExplorerDbContext _dbContext;
        public UpdateJobCommandHandler(IExplorerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Jobs
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Job), request.Id);
            }

            entity.Name = request.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
