namespace Module.Explorer.Domain
{
    public class Category
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int? ParentId { get; set; }
        public Category? Parent { get; set; }

        public required string Name { get; set; }

        public ICollection<Job>? Jobs { get; set; }
        public ICollection<Category>? Categories { get; set; }
    }
}
