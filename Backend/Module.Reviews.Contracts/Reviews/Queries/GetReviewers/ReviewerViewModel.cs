using AutoMapper;
using Module.Companies.Contracts.Companies.Queries.GetCompanyUsers;
using Shared.Mapping;

namespace Module.Reviews.Contracts.Reviews.Queries.GetReviewers
{
    public class ReviewerViewModel : IMapWith<UserViewModel>
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserViewModel, ReviewerViewModel>()
                .ForMember(d => d.Id, m => m.MapFrom(x => x.Id))
                .ForMember(d => d.FirstName, m => m.MapFrom(x => x.FirstName))
                .ForMember(d => d.LastName, m => m.MapFrom(x => x.LastName))
                .ForMember(d => d.Email, m => m.MapFrom(x => x.Email));
        }
    }
}
