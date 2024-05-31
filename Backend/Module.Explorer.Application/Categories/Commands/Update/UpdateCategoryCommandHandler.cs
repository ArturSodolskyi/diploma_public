using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Categories.Commands.Update;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;
using Shared.Exceptions;

namespace Module.Explorer.Application.Categories.Commands.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IExplorerDbContext _dbContext;
        public UpdateCategoryCommandHandler(IExplorerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            entity.Name = request.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
