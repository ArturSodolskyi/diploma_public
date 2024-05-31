using MediatR;

namespace Module.Explorer.Contracts.Tasks.Commands.Create
{
    public class CreateTaskCommand : IRequest<int>
    {
        public int CompetenceId { get; set; }
        public required string Name { get; set; }
    }
}