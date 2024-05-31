using AutoMapper;
using Module.Reviews.Contracts.Reviews.Queries.GetReviewers;
using Shared.Mapping;

namespace WebApi.Models.Dashboard.Companies
{
    public class GetReviewersRequestModel : IMapWith<GetReviewersQuery>
    {
        public int CompanyId { get; set; }
        public required string Filter { get; set; }
        public int Amount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetReviewersRequestModel, GetReviewersQuery>()
                .ForMember(d => d.CompanyId, m => m.MapFrom(x => x.CompanyId))
                .ForMember(d => d.Filter, m => m.MapFrom(x => x.Filter))
                .ForMember(d => d.Amount, m => m.MapFrom(x => x.Amount));
        }
    }
}
