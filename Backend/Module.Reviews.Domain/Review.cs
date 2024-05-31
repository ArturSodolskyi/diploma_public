namespace Module.Reviews.Domain
{
    public class Review
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int RevieweeId { get; set; }
        public int RequestorId { get; set; }
        public int ReviewerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CompanyId { get; set; }

        public ReviewResult? ReviewResult { get; set; }
        public ICollection<ReviewTask>? ReviewTasks { get; set; }

    }
}
