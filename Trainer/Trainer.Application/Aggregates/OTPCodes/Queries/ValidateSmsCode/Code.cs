namespace Trainer.Application.Aggregates.OTPCodes.Queries.ValidateSmsCode
{
    using Newtonsoft.Json;

    public class Code
    {
        public string CodeValue
        {
            get; set;
        }

        public bool IsValid
        {
            get; set;
        }
    }
}
