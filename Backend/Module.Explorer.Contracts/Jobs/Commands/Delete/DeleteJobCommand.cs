using MediatR;

namespace Module.Explorer.Contracts.Jobs.Commands.Delete
{
    public class DeleteJobCommand : IRequest
    {
        public int Id { get; set; }
    }
}
