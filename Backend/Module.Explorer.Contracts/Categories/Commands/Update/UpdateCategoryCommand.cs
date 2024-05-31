using MediatR;

namespace Module.Explorer.Contracts.Categories.Commands.Update
{
    public class UpdateCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}