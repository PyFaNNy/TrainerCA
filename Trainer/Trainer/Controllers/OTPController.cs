using Microsoft.AspNetCore.Mvc;
using Trainer.Application.Aggregates.OTPCodes.Commands.RequestPassword;
using Trainer.Application.Aggregates.OTPCodes.Queries.ValidateSmsCode;
using Trainer.Enums;
using Trainer.Models;

namespace Trainer.Controllers
{
    public class OTPController : BaseController
    {
        public OTPController(ILogger<OTPController> logger) : base(logger)
        {
        }

        [HttpGet]
        public IActionResult ResetPasswordSendEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordSendEmail(RequestPasswordCommand command)
        {
            try
            {
                command.Host = HttpContext.Request.Host.ToString();
                await Mediator.Send(command);
                return RedirectToAction("VerifyCode", "OTP", new { otpAction = OTPAction.ResetPassword, email = command.Email });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("All", ex.Message);
            }

            return View(command);
        }

        [HttpGet]
        public IActionResult VerifyCode(string email, OTPAction otpAction)
        {
            ViewBag.Email = email;
            ViewBag.Action = otpAction;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyCode(ValidateSmsCode code, string email, OTPAction OTPaction)
        {
            var result = await Mediator.Send(new ValidateSmsCodeQuery
            {
                Code = code.Codes,
                Email = email,
                Action = OTPaction
            });
            if (result.IsValid)
            {
                if (OTPaction == OTPAction.ResetPassword)
                {
                    return RedirectToAction("ResetPassword", "BaseUser", new { email });
                }

                if (OTPaction == OTPAction.Registration)
                {
                    return RedirectToAction("ConfirmEmail", "BaseUser", new { email });
                }

                if (OTPaction == OTPAction.Login)
                {
                    return RedirectToAction("ReturnClaim", "Account", new { email });
                }
            }
            else
            {
                ModelState.AddModelError("All", "Wrong Code");
            }

            ViewBag.Email = email;
            ViewBag.Action = OTPaction;
            return View();
        }
    }
}
