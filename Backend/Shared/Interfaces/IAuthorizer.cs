namespace Shared.Interfaces
{
    public interface IAuthorizer<in T>
    {
        Task AuthorizeAsync(T request, CancellationToken cancellationToken = default);
    }
}
