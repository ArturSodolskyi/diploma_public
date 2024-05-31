using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Competencies.Queries.GetCompetencies;
using Module.Explorer.Persistence;

namespace Module.Explorer.Application.Competencies.Queries.GetCompetencies
{
    public class GetCompetenciesQueryHandler : IRequestHandler<GetCompetenciesQuery, List<CompetenceViewModel>>
    {
        private readonly IExplorerDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetCompetenciesQueryHandler(IExplorerDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<CompetenceViewModel>> Handle(GetCompetenciesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Competencies
                .Where(x => x.JobId == request.JobId)
                .Select(x => _mapper.Map<CompetenceViewModel>(x))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}