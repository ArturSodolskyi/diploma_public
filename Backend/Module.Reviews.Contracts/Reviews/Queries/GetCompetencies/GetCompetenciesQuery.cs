using MediatR;
using Module.Explorer.Contracts.Competencies.Queries.GetCompetencies;

namespace Module.Reviews.Contracts.Reviews.Queries.GetCompetencies
{
    public class GetCompetenciesQuery : IRequest<List<CompetenceViewModel>>
    {
        public int ReviewId { get; set; }
    }
}
