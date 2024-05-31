namespace Module.Reviews.Contracts.Reviews.Queries.GetTasks
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Text { get; set; }
        public int Value { get; set; }
    }
}