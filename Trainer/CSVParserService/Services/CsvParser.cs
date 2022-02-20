using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using Trainer.Application.Interfaces;
using Trainer.CSVParserService.Infrastructure;
using Trainer.Domain.Entities.Examination;
using Trainer.Domain.Entities.Patient;

namespace CSVParserService.Services
{
    public class CsvParserService : ICsvParserService
    {
        public async Task<IEnumerable<Examination>> ReadCsvFileToExamination(IFormFile source)
        {
            try
            {
                using (var reader = new StreamReader(source.OpenReadStream()))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Context.RegisterClassMap<ExaminationMap>();
                        var read = csv.Read();
                        var readHeader = csv.ReadHeader();
                        var records = csv.GetRecords<Examination>().ToList();
                        return records;
                    }
                }
            }
            catch (UnauthorizedAccessException e)
            {
                throw new Exception(e.Message);
            }
            catch (FieldValidationException e)
            {
                throw new Exception(e.Message);
            }
            catch (CsvHelperException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Patient>> ReadCsvFileToPatient(IFormFile source)
        {
            try
            {
                using (var reader = new StreamReader(source.OpenReadStream()))
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ";"
                    };
                    using (var csv = new CsvReader(reader, config))
                    {
                        csv.Context.RegisterClassMap<PatientMap>();
                        var read = csv.Read();
                        var readHeader = csv.ReadHeader();
                        var records = csv.GetRecords<Patient>().ToList();
                        return records;
                    }
                }
            }
            catch (UnauthorizedAccessException e)
            {
                throw new Exception(e.Message);
            }
            catch (FieldValidationException e)
            {
                throw new Exception(e.Message);
            }
            catch (CsvHelperException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<byte[]> WriteNewCsvFile(IEnumerable<Patient> patientModels)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(memoryStream))
                {
                    sw.WriteLine("sep=,");
                    using (CsvWriter cw = new CsvWriter(sw, CultureInfo.InvariantCulture))
                    {
                        cw.WriteHeader<Patient>();
                        cw.NextRecord();
                        cw.Context.RegisterClassMap<PatientMap>();
                        cw.WriteRecords(patientModels);
                        cw.Flush();
                        return memoryStream.ToArray();
                    }
                }
            }
            return null;
        }

        public async Task<byte[]> WriteNewCsvFile(IEnumerable<Examination> examinationModels)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(memoryStream))
                {
                    sw.WriteLine("sep=,");
                    using (CsvWriter cw = new CsvWriter(sw, CultureInfo.InvariantCulture))
                    {
                        cw.WriteHeader<Examination>();
                        cw.NextRecord();
                        cw.WriteRecords(examinationModels);
                        cw.Flush();
                        return memoryStream.ToArray();
                    }
                }
            }
            return null;
        }
    }
}
