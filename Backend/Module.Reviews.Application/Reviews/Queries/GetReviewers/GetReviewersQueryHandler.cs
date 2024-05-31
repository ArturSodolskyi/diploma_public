using AutoMapper;
using MediatR;
using Module.Companies.Contracts.Companies.Queries.GetUsers;
using Module.Reviews.Contracts.Reviews.Queries.GetReviewers;

namespace Module.Reviews.Application.Reviews.Queries.GetReviewers
{
    public class GetReviewersQueryHandler : IRequestHandler<GetReviewersQuery, List<ReviewerViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public GetReviewersQueryHandler(IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<List<ReviewerViewModel>> Handle(GetReviewersQuery request, CancellationToken cancellationToken)
        {
            var users = await _mediator.Send(new GetCompanyUsersQuery
            {
                CompanyId = request.CompanyId
            }, cancellationToken);

            return users.Where(x => (x.FirstName + " " + x.LastName + " " + x.Email).Contains(request.Filter))
                .Take(request.Amount)
                .Select(_mapper.Map<ReviewerViewModel>)
                .ToList();
        }
    }
}
