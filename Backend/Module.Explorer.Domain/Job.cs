namespace Module.Explorer.Domain
{
    public class Job
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public required string Name { get; set; }

        public ICollection<Competence>? Competencies { get; set; }
    }
}
