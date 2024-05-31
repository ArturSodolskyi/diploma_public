namespace Module.Reviews.Domain
{
    public class ReviewResult
    {
        public int Id { get; set; }
        public Review? Review { get; set; }

        public required string Comment { get; set; }
    }
}
