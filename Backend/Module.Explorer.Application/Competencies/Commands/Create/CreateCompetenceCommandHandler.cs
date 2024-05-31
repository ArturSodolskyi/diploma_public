using MediatR;
using Module.Explorer.Contracts.Competencies.Commands.Create;
using Module.Explorer.Domain;
using Module.Explorer.Persistence;

namespace Module.Explorer.Application.Competencies.Commands.Create
{
    public class CreateCompetenceCommandHandler : IRequestHandler<CreateCompetenceCommand, int>
    {
        private readonly IExplorerDbContext _dbContext;
        public CreateCompetenceCommandHandler(IExplorerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateCompetenceCommand request, CancellationToken cancellationToken)
        {
            var item = new Competence
            {
                JobId = request.JobId,
                Name = request.Name
            };

            await _dbContext.Competencies.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
    }
}
