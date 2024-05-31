using MediatR;

namespace Module.Explorer.Contracts.Competencies.Queries.GetCompanyId
{
    public class GetCompanyIdQuery : IRequest<int>
    {
        public int CompetenceId { get; set; }
    }
}
