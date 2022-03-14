namespace Trainer.Application.Aggregates.OTPCodes.Queries.ValidateSmsCode
{
    using Newtonsoft.Json;

    public class Code
    {
        [JsonProperty("code")]
        public string CodeValue
        {
            get; set;
        }

        [JsonProperty("isValid")]
        public bool IsValid
        {
            get; set;
        }
    }
}
