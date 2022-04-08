using AutoMapper;
using Trainer.Application.Mappings;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.Patient.Queries.GetPatient
{
    public class Patient : IMapFrom<Domain.Entities.Patient.Patient>
    {
        public Guid Id
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string MiddleName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public int Age
        {
            get;
            set;
        }

        public Sex Sex
        {
            get;
            set;
        }

        public string About
        {
            get;
            set;
        }

        public string Hobbies
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public IList<Result> Results
        {
            get;
            set;
        } = new List<Result>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Patient.Patient, Patient>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.MiddleName, opt => opt.MapFrom(s => s.MiddleName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.Age, opt => opt.MapFrom(s => s.Age))
                .ForMember(d => d.About, opt => opt.MapFrom(s => s.About))
                .ForMember(d => d.Hobbies, opt => opt.MapFrom(s => s.Hobbies));
        }
    }

    public class Result : IMapFrom<Domain.Entities.Result.Result>
    {
        public int AverageHeartRate
        {
            get;
            set;
        }
        public int AverageDia
        {
            get;
            set;
        }

        public int AverageSis
        {
            get;
            set;
        }

        public int AverageOxigen
        {
            get;
            set;
        }
        public double AverageTemperature
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public TypePhysicalActive TypePhysicalActive
        {
            get;
            set;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Result.Result, Result>()
                .ForMember(d => d.AverageTemperature, opt => opt.MapFrom(s => s.AverageTemperature))
                .ForMember(d => d.AverageOxigen, opt => opt.MapFrom(s => s.AverageOxigen))
                .ForMember(d => d.AverageSis, opt => opt.MapFrom(s => s.AverageSis))
                .ForMember(d => d.AverageDia, opt => opt.MapFrom(s => s.AverageDia))
                .ForMember(d => d.Date, opt => opt.MapFrom(s => s.Date))
                .ForMember(d => d.TypePhysicalActive, opt => opt.MapFrom(s => s.TypePhysicalActive))
                .ForMember(d => d.AverageHeartRate, opt => opt.MapFrom(s => s.AverageHeartRate));
        }
    }
}
