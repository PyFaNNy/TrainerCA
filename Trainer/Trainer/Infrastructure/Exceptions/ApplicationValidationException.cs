using Newtonsoft.Json;

namespace Trainer.API.Infrastructure.Exceptions;

public class ApplicationValidationException
{
    [JsonProperty("errors")]
    public IDictionary<string, string[]> Failures
    {
        get;
    }

    [JsonProperty("errorCodes")]
    public IDictionary<string, string[]> ErrorCodes
    {
        get;
    }

    public ApplicationValidationException(IDictionary<string, string[]> failures, IDictionary<string, string[]> errorCodes)
    {
        this.Failures = failures;
        this.ErrorCodes = errorCodes;
    }
}
