using MediatR;

namespace Module.Explorer.Contracts.Competencies.Commands.Delete
{
    public class DeleteCompetenceCommand : IRequest
    {
        public int Id { get; set; }
    }
}
