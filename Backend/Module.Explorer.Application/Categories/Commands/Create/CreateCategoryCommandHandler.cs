using MediatR;
using Module.Explorer.Contracts.Categories.Commands.Create;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;

namespace Module.Explorer.Application.Categories.Commands.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IExplorerDbContext _dbContext;
        public CreateCategoryCommandHandler(IExplorerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var item = new Category
            {
                CompanyId = request.CompanyId,
                ParentId = request.ParentId,
                Name = request.Name
            };
            await _dbContext.Categories.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
    }
}
