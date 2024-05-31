namespace Module.Explorer.Domain
{
    public class Competence
    {
        public int Id { get; set; }

        public int JobId { get; set; }
        public Job? Job { get; set; }

        public required string Name { get; set; }

        public ICollection<CompetenceTask>? Tasks { get; set; }
    }
}
