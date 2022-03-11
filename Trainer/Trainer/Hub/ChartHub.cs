using MediatR;
using Microsoft.AspNetCore.SignalR;
using Trainer.Application.Aggregates.Examination.Commands.FinishExamination;
using Trainer.Application.Aggregates.Examination.Queries.GetExamination;
using Trainer.Application.Aggregates.Results.Commands.Create;

namespace Trainer.Chart
{
    public class ChartHub : Hub
    {
        private readonly IMediator _mediator;
        private readonly Random _rnd = new Random();
        public ChartHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task ProvideReading(string statTermometr, string statTonometr, string statHeartrate, string statOximetr,
            string select1, string select2, string select3, string select4,
            string tonometrValue, string termometrValue, string heartrateValue, string oximetrValue, Guid id)
        {
            bool flag = true;
            var examination = await _mediator.Send(new GetExaminationQuery { ExaminationId = id});
            CountIndicators(examination);

            if (examination.Indicator1 && (select1 != "1" || tonometrValue != "1" || statTonometr != "0"))
            {
                await this.Clients.Caller.SendAsync("error", "Не правильно подключен тонометр");
                flag = false;
            }
            if (examination.Indicator2 && (select2 != "2" || termometrValue != "1" || statTermometr != "0"))
            {
                await this.Clients.Caller.SendAsync("error", "Не правильно подключен термометр");
                flag = false;
            }
            if (examination.Indicator3 && (select3 != "3" || heartrateValue != "1" || statHeartrate != "0"))
            {
                await this.Clients.Caller.SendAsync("error", "Не правильно подключен пульсометр");
                flag = false;
            }
            if (examination.Indicator4 && (select4 != "4" || oximetrValue != "1" || statOximetr != "0"))
            {
                await this.Clients.Caller.SendAsync("error", "Не правильно подключен оксиметр");
                flag = false;
            }

            if (flag)
            {
                uint time = 0;
                int sis = 180;
                int dia = 140;
                int heartRate = 0, averageHeartRate = 0;
                int sep = 0, avarageSep = 0;
                double temperature = 35;

                while (time <= 60)
                {
                    if (examination.Indicator1)
                    {
                        sis -= _rnd.Next(0, 3);
                        dia -= _rnd.Next(0, 2);
                        await this.Clients.Caller.SendAsync("newTonom", time, dia, sis);
                    }
                    if (examination.Indicator2)
                    {
                        temperature += _rnd.Next(0, 20) / 100.0;
                        await this.Clients.Caller.SendAsync("newTermom", time, temperature);
                    }
                    if (examination.Indicator3)
                    {
                        heartRate = _rnd.Next(90, 160);
                        averageHeartRate += heartRate;
                        await this.Clients.Caller.SendAsync("newHearRate", time, heartRate);
                    }
                    if (examination.Indicator4)
                    {
                        sep = _rnd.Next(90, 99);
                        avarageSep += sep;
                        await this.Clients.Caller.SendAsync("newOximetr", time, sep);
                    }
                    time += 1;
                    Thread.Sleep(500);
                }

                await _mediator.Send(new FinishExaminationCommand { ExaminationId = id });
                await _mediator.Send(new CreateResultsCommand
                {
                    AverageDia = dia,
                    AverageSis = sis,
                    AverageTemperature = temperature,
                    AverageOxigen = avarageSep/60,
                    AverageHeartRate =averageHeartRate/60,
                    ExaminationId = examination.Id,
                    PatientId = examination.Patient.Id,
                });
            }
        }

        public async Task TestTonomert()
        {
            int count = _rnd.Next(0, 3);
            string status = string.Empty;
            if (count == 0)
            {
                status = "исправен";
            }
            if (count == 1)
            {
                status = "неисправен";
            }
            if (count == 2)
            {
                status = "занят";
            }
            await this.Clients.Caller.SendAsync("statusTonometr", status, count);
        }

        public async Task TestTermometr()
        {
            int count = _rnd.Next(0, 3);
            string status = string.Empty;
            if (count == 0)
            {
                status = "исправен";
            }
            if (count == 1)
            {
                status = "неисправен";
            }
            if (count == 2)
            {
                status = "занят";
            }
            await this.Clients.Caller.SendAsync("statusTermometr", status, count);
        }

        public async Task TestHeartrate()
        {
            int count = _rnd.Next(0, 3);
            string status = string.Empty;
            if (count == 0)
            {
                status = "исправен";
            }
            if (count == 1)
            {
                status = "неисправен";
            }
            if (count == 2)
            {
                status = "занят";
            }
            await this.Clients.Caller.SendAsync("statusHeartrate", status, count);
        }

        public async Task TestOximetr()
        {
            int count = _rnd.Next(0, 3);
            string status = string.Empty;
            if (count == 0)
            {
                status = "исправен";
            }
            if (count == 1)
            {
                status = "неисправен";
            }
            if (count == 2)
            {
                status = "занят";
            }
            await this.Clients.Caller.SendAsync("statusOximetr", status, count);
        }

        private void CountIndicators(Examination model)
        {
            var temp = model.Indicators;
            if (temp - 8 >= 0)
            {
                temp -= 8;
                model.Indicator4 = true;
            }
            if (temp - 4 >= 0)
            {
                temp -= 4;
                model.Indicator3 = true;
            }
            if (temp - 2 >= 0)
            {
                temp -= 2;
                model.Indicator2 = true;
            }
            if (temp - 1 >= 0)
            {
                temp -= 1;
                model.Indicator1 = true;
            }
        }
    }
}
