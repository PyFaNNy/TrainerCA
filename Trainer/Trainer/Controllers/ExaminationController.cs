using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Trainer.Controllers
{

    public class ExaminationController : Controller
    {
        private readonly IContextService _contextService;
        private readonly IMailService _mailService;
        private readonly ICsvParserService _csvService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ExaminationValidator _validator;
        private readonly IHubContext<ChartHub> _chartHub;
        private readonly IWebHostEnvironment _appEnvironment;
        public ExaminationController(IContextService serv, ExaminationValidator validator, IMapper mapper, IHubContext<ChartHub> chartHub, IMailService mailService,
                                     UserManager<User> userManager, ICsvParserService csv, IWebHostEnvironment app)
        {
            _contextService = serv ?? throw new ArgumentNullException($"{nameof(serv)} is null.");
            _validator = validator ?? throw new ArgumentNullException($"{nameof(validator)} is null.");
            _mapper = mapper ?? throw new ArgumentNullException($"{nameof(mapper)} is null.");
            _chartHub = chartHub ?? throw new ArgumentNullException($"{nameof(chartHub)} is null.");
            _mailService = mailService ?? throw new ArgumentNullException($"{nameof(mailService)} is null.");
            _userManager = userManager ?? throw new ArgumentNullException($"{nameof(userManager)} is null.");
            _csvService = csv ?? throw new ArgumentNullException($"{nameof(csv)} is null.");
            _appEnvironment = app ?? throw new ArgumentNullException($"{nameof(app)} is null.");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Doctor,Manager")]
        public async Task<IActionResult> GetModels(SortState sortOrder = SortState.FirstNameSort)
        {
            ViewData["DateSort"] = sortOrder == SortState.DateSort ? SortState.DateSortDesc : SortState.DateSort;
            ViewData["TypeSort"] = sortOrder == SortState.TypeSort ? SortState.TypeSortDesc : SortState.TypeSort;
            ViewData["FirstNameSort"] = sortOrder == SortState.FirstNameSort ? SortState.FirstNameSortDesc : SortState.FirstNameSort;
            ViewData["LastNameSort"] = sortOrder == SortState.LastNameSort ? SortState.LastNameSortDesc : SortState.LastNameSort;
            ViewData["MiddleNameSort"] = sortOrder == SortState.MiddleNameSort ? SortState.MiddleNameSortDesc : SortState.MiddleNameSort;

            IEnumerable<ExaminationDTO> examinationDtos = await _contextService.GetExaminations(sortOrder);
            var examinations = _mapper.Map<List<ExaminationViewModel>>(examinationDtos);
            return View(examinations);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Doctor,Manager")]
        public async Task<IActionResult> GetModel(Guid id)
        {
            try
            {
                ExaminationDTO examinationDTO = await _contextService.GetExamination(id);
                var examinationView = _mapper.Map<ExaminationViewModel>(examinationDTO);
                ViewBag.Id = examinationView.Id;
                InvCountIndicators(examinationView);
                return View(examinationView);
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> AddModel(Guid id)
        {
            ViewBag.UserId = id;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> AddModel(ExaminationViewModel model, Guid patientid)
        {
            try
            {
                CountIndicators(model);
                _validator.ValidateAndThrow(model);
                var examinationDto = _mapper.Map<ExaminationDTO>(model);
                await _contextService.Create(examinationDto);
                var patient = await _contextService.GetPatient(examinationDto.PatientId);
                var doctor = await _userManager.GetUserAsync(HttpContext.User);
                var template = Template.Parse(Resource.Examination);
                var body = template.Render(new
                {
                    patient = patient,
                    model = model
                });
                await _mailService.SendEmailAsync(new MailRequest
                {
                    ToEmail= patient.Email,
                    Body=body,
                    Subject= $"Set Examination by {doctor?.FirstName}"
                });
                return RedirectToAction("GetModels");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> UpdateModel(Guid id)
        {
            ExaminationDTO examinationDTO = await _contextService.GetExamination(id);
            var examinationView = _mapper.Map<ExaminationViewModel>(examinationDTO);
            InvCountIndicators(examinationView);
            ViewBag.Examination = examinationView;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> UpdateModel(ExaminationViewModel model, Guid patientid)
        {
            try
            {
                model.Date = DateTime.UtcNow;
                CountIndicators(model);
                _validator.ValidateAndThrow(model);
                var examinationDto = _mapper.Map<ExaminationDTO>(model);
                await _contextService.Update(examinationDto);

                var patient = await _contextService.GetPatient(examinationDto.PatientId);
                var doctor = await _userManager.GetUserAsync(HttpContext.User);
                var template = Template.Parse(Resource.Examination);
                var body = template.Render(new
                {
                    patient = patient,
                    model = model
                });
                await _mailService.SendEmailAsync(new MailRequest
                {
                    ToEmail = patient.Email,
                    Body = body,
                    Subject = $"Update Examination by {doctor?.FirstName}"
                });

                return RedirectToAction("GetModels");
            }
            catch (RecordNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Doctor,Admin")]
        public async Task<RedirectToActionResult> DeleteModel(Guid[] selectedExamination)
        {
            foreach (var examination in selectedExamination)
            {
                await _contextService.DeleteExamination(examination);
            }
            return RedirectToAction("GetModels");
        }

        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> ExportToCSV()
        {
            try
            {
                IEnumerable<ExaminationDTO> examinatiomDTO = await _contextService.GetExaminations(SortState.FirstNameSort);
                var memoryStream =await _csvService.WriteNewCsvFile(examinatiomDTO);
                return File(memoryStream, "text/csv", fileDownloadName: "Examinations.csv");
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> ImportToCSV()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> ImportToCSV(CSV source)
        {
            try
            {
                var examinations = await _csvService.ReadCsvFileToExamination(source.File);
                await _contextService.Range(examinations);
                return RedirectToAction("GetModels");
            }
            catch (Exception e)
            {

                throw;
            }
        }

        private void CountIndicators(ExaminationViewModel model)
        {
            model.Indicators = 0;
            if (model.Indicator1)
            {
                model.Indicators += 1;
            }
            if (model.Indicator2)
            {
                model.Indicators += 2;
            }
            if (model.Indicator3)
            {
                model.Indicators += 4;
            }
            if (model.Indicator4)
            {
                model.Indicators += 8;
            }
        }

        private void InvCountIndicators(ExaminationViewModel model)
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
