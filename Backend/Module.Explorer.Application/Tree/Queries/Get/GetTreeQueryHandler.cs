using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Explorer.Contracts.Tree.Queries.Get;
using Module.Explorer.Persistence;
using Module.Users.Contracts.CurrentUser.Queries.GetCompanyId;

namespace Module.Explorer.Application.Tree.Queries.Get
{
    public class GetTreeQueryHandler : IRequestHandler<GetTreeQuery, TreeViewModel>
    {
        private readonly IExplorerDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public GetTreeQueryHandler(IExplorerDbContext dbContext,
            IMapper mapper,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<TreeViewModel> Handle(GetTreeQuery request, CancellationToken cancellationToken)
        {
            var result = new TreeViewModel();
            var companyId = await _mediator.Send(new GetCompanyIdQuery(), cancellationToken);
            if (companyId == null)
            {
                return result;
            }

            result.Categories = await _dbContext.Categories
                .Where(x => x.CompanyId == companyId)
                .Select(x => _mapper.Map<CategoryViewModel>(x))
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            result.Jobs = await _dbContext.Jobs
                .Where(x => x.CompanyId == companyId)
                .Select(x => _mapper.Map<JobViewModel>(x))
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
