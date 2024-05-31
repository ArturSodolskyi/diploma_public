namespace Module.Reviews.Contracts.Reviews.Queries.GetUserReviews
{
    public class UserReviewViewModel
    {
        public required string JobName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Coverage { get; set; }
    }
}
