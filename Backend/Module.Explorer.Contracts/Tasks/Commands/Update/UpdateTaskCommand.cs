using MediatR;

namespace Module.Explorer.Contracts.Tasks.Commands.Update
{
    public class UpdateTaskCommand : IRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Text { get; set; }
    }
}