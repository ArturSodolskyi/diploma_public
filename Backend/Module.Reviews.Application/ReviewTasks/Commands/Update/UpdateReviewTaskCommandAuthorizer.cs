using MediatR;
using Module.Companies.Contracts.Common;
using Module.Companies.Contracts.UserCompanies.Queries.IsInCompanyRole;
using Module.Reviews.Contracts.Reviews.Queries.GetCompanyId;
using Module.Reviews.Contracts.ReviewTasks.Commands.Update;
using Shared.Accessors;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Module.Reviews.Application.ReviewTasks.Commands.Update
{
    public class UpdateReviewTaskCommandAuthorizer : IAuthorizer<UpdateReviewTaskCommand>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public UpdateReviewTaskCommandAuthorizer(IUserAccessor userAccessor,
            IMediator mediator)
        {
            _userAccessor = userAccessor;
            _mediator = mediator;
        }

        public async Task AuthorizeAsync(UpdateReviewTaskCommand request, CancellationToken cancellationToken = default)
        {
            if (_userAccessor.IsAdministrator)
            {
                return;
            }

            var companyId = await _mediator.Send(new GetCompanyIdQuery
            {
                ReviewId = request.ReviewId
            }, cancellationToken);

            var isAdministrator = await _mediator.Send(new IsInCompanyRoleQuery
            {
                CompanyId = companyId,
                UserId = _userAccessor.UserId,
                Role = CompanyRoleEnum.Administrator
            }, cancellationToken);

            if (!isAdministrator)
            {
                throw new ForbiddenException();
            }
        }
    }
}