namespace Module.Reviews.Domain
{
    public class ReviewTask
    {
        public int Value { get; set; }

        public int ReviewId { get; set; }
        public Review? Review { get; set; }

        public int TaskId { get; set; }
    }
}
