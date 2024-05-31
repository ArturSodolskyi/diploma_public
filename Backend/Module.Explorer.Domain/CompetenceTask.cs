namespace Module.Explorer.Domain
{
    public class CompetenceTask
    {
        public int Id { get; set; }

        public int CompetenceId { get; set; }
        public Competence? Competence { get; set; }

        public required string Name { get; set; }
        public required string Text { get; set; }
    }
}
