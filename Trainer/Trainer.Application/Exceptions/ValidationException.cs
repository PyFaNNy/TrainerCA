using FluentValidation.Results;

namespace Trainer.Application.Exceptions
{
    using System;
    using System.Collections.Generic;

    public class ValidationException : Exception
    {
        public IDictionary<string, string> Errors
        {
            get;
        }

        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            this.Errors = new Dictionary<string, string>();
        }

        public ValidationException(string propertyName, string errorMessage)
            : this()
        {
            this.Errors.Add(propertyName, errorMessage);
        }
    }
}
