namespace Shared.Exceptions
{
    public class ValidationException : Exception
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; }

        public ValidationException(IReadOnlyDictionary<string, string[]> errors)
            : base("Validation failure. One or more validation errors occurred")
        {
            Errors = errors;
        }
    }
}
