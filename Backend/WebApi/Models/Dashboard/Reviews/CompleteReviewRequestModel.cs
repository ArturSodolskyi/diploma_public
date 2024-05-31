using AutoMapper;
using Module.Reviews.Contracts.Reviews.Commands.CompleteReview;
using Shared.Mapping;

namespace WebApi.Models.Dashboard.Reviews
{
    public class CompleteReviewRequestModel : IMapWith<CompleteReviewCommand>
    {
        public int ReviewId { get; set; }
        public required string Comment { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CompleteReviewRequestModel, CompleteReviewCommand>()
                .ForMember(d => d.ReviewId, m => m.MapFrom(x => x.ReviewId))
                .ForMember(d => d.Comment, m => m.MapFrom(x => x.Comment));
        }
    }
}
