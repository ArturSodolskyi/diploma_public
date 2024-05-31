using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Reviews.Contracts.Reviews.Queries.GetReviews;
using Module.Reviews.Persistence;
using Module.Users.Contracts.CurrentUser.Queries.GetCompanyId;
using Shared.Accessors;

namespace Module.Reviews.Application.Reviews.Queries.GetReviews
{
    public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, List<ReviewViewModel>>
    {
        private readonly IReviewsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public GetReviewsQueryHandler(IReviewsDbContext dbContext,
            IMapper mapper,
            IUserAccessor userAccessor,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task<List<ReviewViewModel>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            var userId = _userAccessor.UserId;
            var companyId = await _mediator.Send(new GetCompanyIdQuery(), cancellationToken);

            return await _dbContext.Reviews
                .Where(x => x.CompanyId == companyId && (x.RevieweeId == userId || x.ReviewerId == userId))
                .Select(x => _mapper.Map<ReviewViewModel>(x))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
