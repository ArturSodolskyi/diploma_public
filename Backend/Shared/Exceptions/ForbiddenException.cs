namespace Shared.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base($"Forbidden.")
        {

        }
    }
}
