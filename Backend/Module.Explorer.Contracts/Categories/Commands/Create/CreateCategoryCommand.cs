using MediatR;

namespace Module.Explorer.Contracts.Categories.Commands.Create
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public int CompanyId { get; set; }
        public int? ParentId { get; set; }
        public required string Name { get; set; }
    }
}
