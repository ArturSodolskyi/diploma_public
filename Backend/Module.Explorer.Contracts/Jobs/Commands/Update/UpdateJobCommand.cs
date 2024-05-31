using MediatR;

namespace Module.Explorer.Contracts.Jobs.Commands.Update
{
    public class UpdateJobCommand : IRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}