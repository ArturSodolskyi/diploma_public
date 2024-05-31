using MediatR;

namespace Module.Explorer.Contracts.Jobs.Queries.GetCompanyId
{
    public class GetCompanyIdQuery : IRequest<int>
    {
        public int JobId { get; set; }
    }
}
