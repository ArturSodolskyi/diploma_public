using MediatR;

namespace Module.Explorer.Contracts.Tasks.Commands.Delete
{
    public class DeleteTaskCommand : IRequest
    {
        public int Id { get; set; }
    }
}