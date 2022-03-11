using AutoMapper;
using Trainer.Application.Mappings;
using Trainer.Enums;

namespace Trainer.Application.Aggregates.Examination.Queries.GetExamination
{
    public class Examination : IMapFrom<Domain.Entities.Examination.Examination>
    {
        public Guid Id
        {
            get;
            set;
        }

        public TypePhysicalActive TypePhysicalActive
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public bool Indicator1
        {
            get;
            set;
        }

        public bool Indicator2
        {
            get;
            set;
        }

        public bool Indicator3
        {
            get;
            set;
        }

        public bool Indicator4
        {
            get;
            set;
        }

        public int Indicators
        {
            get;
            set;
        }
        
        public Patient Patient
        {
            get;
            set;
        }

        public ExaminationStatus Status
        {
            get;
            set;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Examination.Examination, Examination>()
                .ForMember(d => d.Date, opt => opt.MapFrom(s => s.Date))
                .ForMember(d => d.Patient, opt => opt.MapFrom(s => s.Patient))
                .ForMember(d => d.TypePhysicalActive, opt => opt.MapFrom(s => s.TypePhysicalActive))
                .ForMember(d => d.Indicators, opt => opt.MapFrom(s => s.Indicators))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status));
        }
    }

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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Patient.Patient, Patient>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.MiddleName, opt => opt.MapFrom(s => s.MiddleName))
                .ForMember(d => d.Sex, opt => opt.MapFrom(s => s.Sex))
                .ForMember(d => d.Age, opt => opt.MapFrom(s => s.Age));
        }
    }
}
