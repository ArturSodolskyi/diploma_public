using MediatR;

namespace Module.Explorer.Contracts.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; set; }
    }
}
