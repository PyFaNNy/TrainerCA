using CsvHelper.Configuration;
using Trainer.Domain.Entities.Examination;

namespace Trainer.CSVParserService.Infrastructure
{
    public class ExaminationMap : ClassMap<Examination>
    {
        public ExaminationMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.Date).Name("Date");
            Map(m => m.TypePhysicalActive).Name("TypePhysicalActive");
            Map(m => m.Indicators).Name("Indicators");
            Map(m => m.Status).Name("Status");
            Map(m => m.PatientId).Name("PatientId");
        }
    }
}
