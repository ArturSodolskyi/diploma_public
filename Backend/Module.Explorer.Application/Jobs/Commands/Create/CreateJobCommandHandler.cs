using MediatR;
using Module.Explorer.Contracts.Jobs.Commands.Create;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;

namespace Module.Explorer.Application.Jobs.Commands.Create
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, int>
    {
        private readonly IExplorerDbContext _dbContext;
        public CreateJobCommandHandler(IExplorerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var item = new Job
            {
                CompanyId = request.CompanyId,
                CategoryId = request.CategoryId,
                Name = request.Name
            };
            await _dbContext.Jobs.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
    }
}
