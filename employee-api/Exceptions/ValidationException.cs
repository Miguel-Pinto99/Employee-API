using FluentValidation.Results;

namespace employee_api.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
        }
        public ValidationException(List<ValidationFailure> failures)
            : this()
        {
            IEnumerable<string> propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();
            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();
                Failures.Add(propertyName, propertyFailures);
            }
        }
        public IDictionary<string, string[]> Failures { get; }
    }
}