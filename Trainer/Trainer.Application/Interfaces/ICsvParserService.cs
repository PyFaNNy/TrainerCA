using Microsoft.AspNetCore.Http;
using Trainer.Domain.Entities.Examination;
using Trainer.Domain.Entities.Patient;

namespace Trainer.Application.Interfaces
{
    public interface ICsvParserService
    {
        Task<IEnumerable<Patient>> ReadCsvFileToPatient(IFormFile source);
        Task<IEnumerable<Examination>> ReadCsvFileToExamination(IFormFile source);
        Task<byte[]> WriteNewCsvFile(IEnumerable<Patient> employeeModels);
        Task<byte[]> WriteNewCsvFile(IEnumerable<Examination> employeeModels);
    }
}
