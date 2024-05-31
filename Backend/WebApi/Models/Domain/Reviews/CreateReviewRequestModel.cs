using AutoMapper;
using Module.Reviews.Contracts.Reviews.Commands.Create;
using Shared.Mapping;

namespace WebApi.Models.Domain.Reviews
{
    public class CreateReviewRequestModel : IMapWith<CreateReviewCommand>
    {
        public int JobId { get; set; }
        public int RevieweeId { get; set; }
        public int ReviewerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateReviewRequestModel, CreateReviewCommand>()
                .ForMember(d => d.JobId, m => m.MapFrom(x => x.JobId))
                .ForMember(d => d.RevieweeId, m => m.MapFrom(x => x.RevieweeId))
                .ForMember(d => d.ReviewerId, m => m.MapFrom(x => x.ReviewerId));
        }
    }
}
