using Trainer.Enums;

namespace Trainer.Application.Aggregates.CSV.Queries.PatientsToCSV
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

        public ContentType Type
        {
            get;
            set;
        }
    }
}
