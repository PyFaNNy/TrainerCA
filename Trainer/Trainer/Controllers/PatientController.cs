using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trainer.Application.Aggregates.CSV.Queries.PatientToCSV;
using Trainer.Application.Aggregates.Patient.Commands.CreatePatient;
using Trainer.Application.Aggregates.Patient.Commands.DeletePatient;
using Trainer.Application.Aggregates.Patient.Commands.UpdatePatient;
using Trainer.Application.Aggregates.Patient.Queries.GetPatient;
using Trainer.Application.Aggregates.Patient.Queries.GetPatients;
using Trainer.Application.Interfaces;
using Trainer.Enums;

namespace Trainer.Controllers
{
    public class PatientController : BaseController
    {
        private readonly ICsvParserService _csvService;

        public PatientController(ILogger<PatientController> logger, IMapper mapper, ICsvParserService csv)
            : base(logger)
        {
            _csvService = csv ?? throw new ArgumentNullException($"{nameof(csv)} is null.");
        }

        [HttpGet]
        [Authorize(Roles = "admin, doctor, manager")]
        public async Task<IActionResult> GetModels(SortState sortOrder = SortState.FirstNameSort)
        {
            ViewData["FirstNameSort"] = sortOrder == SortState.FirstNameSort ? SortState.FirstNameSortDesc : SortState.FirstNameSort;
            ViewData["LastNameSort"] = sortOrder == SortState.LastNameSort ? SortState.LastNameSortDesc : SortState.LastNameSort;
            ViewData["MiddleNameSort"] = sortOrder == SortState.MiddleNameSort ? SortState.MiddleNameSortDesc : SortState.MiddleNameSort;
            ViewData["AgeSort"] = sortOrder == SortState.AgeSort ? SortState.AgeSortDesc : SortState.AgeSort;
            ViewData["SexSort"] = sortOrder == SortState.SexSort ? SortState.SexSortDesc : SortState.SexSort;

            var results = await Mediator.Send(new GetPatientsQuery { SortOrder = sortOrder });
            return View(results);
        }

        [HttpGet]
        [Authorize(Roles = "admin, doctor, manager")]
        public async Task<IActionResult> GetModel(Guid id)
        {
            var patient = await Mediator.Send(new GetPatientQuery { PatientId = id });
            ViewBag.Results = patient.Results.Take(5);
            return View(patient);
        }

        [HttpGet]
        [Authorize(Roles = "admin, manager")]
        public IActionResult AddModel()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> AddModel(CreatePatientCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("GetModels");
        }

        [HttpGet]
        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> UpdateModel(Guid id)
        {
            var patient = await Mediator.Send(new GetPatientQuery { PatientId = id });
            ViewBag.Patient = patient;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> UpdateModel(UpdatePatientCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "admin, manager")]
        public async Task<RedirectToActionResult> DeleteModelAsync(Guid[] selectedPatient)
        {
            await Mediator.Send(new DeletePatientsCommand { PatientsId = selectedPatient });
            return RedirectToAction("GetModels");
        }

        [HttpGet]
        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> ExportToCSV()
        {
            var fileInfo = await Mediator.Send(new PatientToCSVQuery());
            return File(fileInfo.Content, "text/csv", fileInfo.FileName);
        }

        [HttpGet]
        public async Task<IActionResult> ImportToCSV()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> ImportToCSV(CSV source)
        {
            var patients = await _csvService.ReadCsvFileToPatient(source.File);
            await _contextService.Range(patients);
            return RedirectToAction("GetModels");
        }
    }
}
