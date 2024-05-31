using AutoMapper;
using Module.Reviews.Contracts.ReviewTasks.Commands.Update;
using Shared.Mapping;

namespace WebApi.Models.Domain.ReviewTasks
{
    public class UpdateReviewTaskRequestModel : IMapWith<UpdateReviewTaskCommand>
    {
        public int ReviewId { get; set; }
        public int TaskId { get; set; }
        public int Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateReviewTaskRequestModel, UpdateReviewTaskCommand>()
                .ForMember(d => d.ReviewId, m => m.MapFrom(x => x.ReviewId))
                .ForMember(d => d.TaskId, m => m.MapFrom(x => x.TaskId))
                .ForMember(d => d.Value, m => m.MapFrom(x => x.Value));
        }
    }
}
