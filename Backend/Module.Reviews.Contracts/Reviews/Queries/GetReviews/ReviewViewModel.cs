using AutoMapper;
using Module.Reviews.Domain;
using Shared.Mapping;

namespace Module.Reviews.Contracts.Reviews.Queries.GetReviews
{
    public class ReviewViewModel : IMapWith<Review>
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int RevieweeId { get; set; }
        public int ReviewerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Review, ReviewViewModel>()
                .ForMember(d => d.Id, m => m.MapFrom(x => x.Id))
                .ForMember(d => d.StartDate, m => m.MapFrom(x => x.StartDate))
                .ForMember(d => d.EndDate, m => m.MapFrom(x => x.EndDate))
                .ForMember(d => d.RevieweeId, m => m.MapFrom(x => x.RevieweeId))
                .ForMember(d => d.ReviewerId, m => m.MapFrom(x => x.ReviewerId));
        }
    }
}
