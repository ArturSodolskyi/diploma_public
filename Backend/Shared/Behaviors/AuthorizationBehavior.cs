using MediatR;
using Shared.Interfaces;

namespace Shared.Behaviors
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseRequest
    {
        private readonly IEnumerable<IAuthorizer<TRequest>> _authorizers;

        public AuthorizationBehavior(IEnumerable<IAuthorizer<TRequest>> authorizers)
        {
            _authorizers = authorizers;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            foreach (var authorizer in _authorizers)
            {
                await authorizer.AuthorizeAsync(request, cancellationToken);
            }

            return await next();
        }
    }
}
