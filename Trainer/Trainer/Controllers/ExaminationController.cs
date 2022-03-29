using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trainer.Application.Exceptions;
using Trainer.Application.Aggregates.CSV.Commands.CSVToExaminations;
using Trainer.Application.Aggregates.CSV.Queries.ExaminationsToCSV;
using Trainer.Application.Aggregates.Examination.Commands.CreateExamination;
using Trainer.Application.Aggregates.Examination.Commands.DeleteExamination;
using Trainer.Application.Aggregates.Examination.Commands.UpdateExamination;
using Trainer.Application.Aggregates.Examination.Queries.GetExamination;
using Trainer.Application.Aggregates.Examination.Queries.GetExaminations;
using Trainer.Application.Interfaces;
using Trainer.Common;
using Trainer.Enums;
using Trainer.Infrastructure.Extensions;
using Trainer.Models;

namespace Trainer.Controllers
{

    public class ExaminationController : BaseController
    {
        public ExaminationController(ILogger<ExaminationController> logger, ICsvParserService csv)
            : base(logger)
        {
        }

        [HttpGet]
        [Authorize(Roles = "admin, doctor, manager")]
        public async Task<IActionResult> GetModels(
            SortState sortOrder = SortState.FirstNameSort,
            int? pageIndex = 1,
            int? pageSize = 10)
        {
            ViewData["DateSort"] = sortOrder == SortState.DateSort ? SortState.DateSortDesc : SortState.DateSort;
            ViewData["TypeSort"] = sortOrder == SortState.TypeSort ? SortState.TypeSortDesc : SortState.TypeSort;
            ViewData["FirstNameSort"] = sortOrder == SortState.FirstNameSort ? SortState.FirstNameSortDesc : SortState.FirstNameSort;
            ViewData["LastNameSort"] = sortOrder == SortState.LastNameSort ? SortState.LastNameSortDesc : SortState.LastNameSort;
            ViewData["MiddleNameSort"] = sortOrder == SortState.MiddleNameSort ? SortState.MiddleNameSortDesc : SortState.MiddleNameSort;

            var result = await Mediator.Send(new GetExaminationsQuery(pageIndex, pageSize, sortOrder));
            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "admin, doctor, manager")]
        public async Task<IActionResult> GetModel(Guid id)
        {
            var result = await Mediator.Send(new GetExaminationQuery { ExaminationId = id });
            ViewBag.Id = result.Id;
            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "doctor")]
        public async Task<IActionResult> AddModel(Guid id)
        {
            ViewBag.UserId = id;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "doctor")]
        public async Task<IActionResult> AddModel(CreateExaminationCommand command)
        {
            try
            {
                var doctorId = this.HttpContext.User.GetUserId();
                command.DoctorId = doctorId.Value;
                await Mediator.Send(command);

                return RedirectToAction("GetModels");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("All", ex.Errors.FirstOrDefault().Value);
            }
            catch (Exception ex)
            {

            }

            ViewBag.UserId = command.PatientId;
            return View(command);
        }

        [HttpGet]
        [Authorize(Roles = "doctor")]
        public async Task<IActionResult> UpdateModel(Guid id)
        {
            var examination = await Mediator.Send(new GetExaminationQuery { ExaminationId = id });
            ViewBag.Examination = examination;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "doctor")]
        public async Task<IActionResult> UpdateModel(UpdateExaminationCommand command)
        {
            try
            {
                var doctorId = this.HttpContext.User.GetUserId();
                command.DoctorId = doctorId.Value;
                await Mediator.Send(command);

                return RedirectToAction("GetModels");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("All", ex.Errors.FirstOrDefault().Value);
            }
            catch (Exception ex)
            {

            }
            ViewBag.Examination = command;
            return View(command);
        }

        [Authorize(Roles = "doctor")]
        public async Task<RedirectToActionResult> DeleteModel(Guid[] selectedExamination)
        {
            await Mediator.Send(new DeleteExaminationsCommand { ExaminationsId = selectedExamination });
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> ExportToCSV()
        {
            var fileInfo =await Mediator.Send(new ExaminationsToCSVQuery());
            return File(fileInfo.Content, fileInfo.Type.ToName(), fileInfo.FileName);
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
            await Mediator.Send(new CSVToExaminationsCommand { CSVFile = source.File });
            return RedirectToAction("GetModels");
        }
    }
}
