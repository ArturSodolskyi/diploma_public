using MediatR;

namespace Module.Explorer.Contracts.Competencies.Commands.Update
{
    public class UpdateCompetenceCommand : IRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}