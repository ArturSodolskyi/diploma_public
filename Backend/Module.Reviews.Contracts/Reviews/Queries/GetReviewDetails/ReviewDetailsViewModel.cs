namespace Module.Reviews.Contracts.Reviews.Queries.GetReviewDetails
{
    public class ReviewDetailsViewModel
    {
        public required string Job { get; set; }
        public required string Reviewee { get; set; }
        public required string Reviewer { get; set; }
        public required string Requestor { get; set; }
    }
}
