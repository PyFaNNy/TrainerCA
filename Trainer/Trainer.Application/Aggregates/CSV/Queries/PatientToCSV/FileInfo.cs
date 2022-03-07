namespace Trainer.Application.Aggregates.CSV.Queries.PatientToCSV
{
    public class FileInfo
    {
        public string FileName
        {
            get;
            set;
        }

        public byte[] Content
        {
            get;
            set;
        }
    }
}
