namespace Trainer.Application.Exceptions
{
    using System;
    using System.Collections.Generic;

    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            this.Errors = new Dictionary<string, string>();
        }

        public IDictionary<string, string> Errors
        {
            get;
        }

        public ValidationException(string propertyName, string errorMessage)
           : this()
        {
            this.Errors.Add(propertyName, errorMessage);
        }
    }
}
