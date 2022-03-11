using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainer.Application.Mappings;

namespace Trainer.Application.Aggregates.Results.Commands.Create
{
    public class CreateResultsCommand : IRequest<Unit>, IMapTo<Domain.Entities.Result.Result>
    {
        public Guid Id
        {
            get;
            set;
        }

        public Guid PatientId
        {
            get;
            set;
        }

        public Guid ExaminationId
        {
            get;
            set;
        }

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
    }
}
