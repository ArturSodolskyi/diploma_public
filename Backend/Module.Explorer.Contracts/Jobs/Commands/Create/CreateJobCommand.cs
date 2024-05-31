using MediatR;

namespace Module.Explorer.Contracts.Jobs.Commands.Create
{
    public class CreateJobCommand : IRequest<int>
    {
        public int CompanyId { get; set; }
        public int? CategoryId { get; set; }
        public required string Name { get; set; }
    }
}
