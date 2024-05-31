using MediatR;

namespace Module.Explorer.Contracts.Competencies.Commands.Create
{
    public class CreateCompetenceCommand : IRequest<int>
    {
        public int JobId { get; set; }
        public required string Name { get; set; }
    }
}
