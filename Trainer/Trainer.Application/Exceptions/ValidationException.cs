namespace Trainer.Application.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation.Results;

    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            this.Errors = new Dictionary<string, string[]>();
            this.ErrorCodes = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            this.Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

            this.ErrorCodes = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorCode)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

        }

        public IDictionary<string, string[]> Errors
        {
            get;
        }

        public IDictionary<string, string[]> ErrorCodes
        {
            get;
        }

        public ValidationException(string propertyName, string errorMessage)
           : this()
        {
            this.Errors.Add(propertyName, new string[] { errorMessage });
            this.ErrorCodes.Add(propertyName, new string[] { propertyName });
        }

        public ValidationException(string propertyName, string errorCode, string errorMessage) : this()
        {
            this.ErrorCodes.Add(propertyName, new [] {errorCode});
            this.Errors.Add(propertyName, new [] { errorMessage });
        }
    }
}
