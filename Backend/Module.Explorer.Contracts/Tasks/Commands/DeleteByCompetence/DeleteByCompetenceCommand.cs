using MediatR;

namespace Module.Explorer.Contracts.Tasks.Commands.DeleteByCompetence
{
    public class DeleteByCompetenceCommand : IRequest
    {
        public int CompetenceId { get; set; }
    }
}