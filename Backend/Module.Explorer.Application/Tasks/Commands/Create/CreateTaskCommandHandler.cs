using MediatR;
using Module.Explorer.Contracts.Tasks.Commands.Create;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;

namespace Module.Explorer.Application.Tasks.Commands.Create
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly IExplorerDbContext _dbContext;
        public CreateTaskCommandHandler(IExplorerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var item = new CompetenceTask
            {
                CompetenceId = request.CompetenceId,
                Name = request.Name,
                Text = ""
            };
            await _dbContext.Tasks.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
    }
}
