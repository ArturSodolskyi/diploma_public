using MediatR;

namespace Module.Explorer.Contracts.Competencies.Queries.GetCompetencies
{
    public class GetCompetenciesQuery : IRequest<List<CompetenceViewModel>>
    {
        public int JobId { get; set; }
    }
}
