using DataAccessLayer.Repository.IRepository_Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Patient_Appointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailRepository _mailService;
       
        public MailController(IMailRepository _MailService)
        {
            _mailService = _MailService;
        }

        [HttpPost]
        public IActionResult GetByEmail(string request)
        {
            _mailService.GetByEmail(request);
            return Ok();
        }
        [HttpPost("Doctor")]
        public IActionResult GetByEmailDoctor(string request)
        {
            _mailService.GetByEmailDoctor(request);
            return Ok();
        }
        [HttpPost("ApproveEmail")]
        public IActionResult ApproveEmail(string request)
        {
            _mailService.ApproveEmail(request);
            return Ok();
        }
        [HttpPost("DeclineEmail")]
        public IActionResult SendDeclineEmail(string request)
        {
            _mailService.SendDeclineEmail(request);
            return Ok();
        }
        [HttpPost(" SendRescheduleEmail")]
        public IActionResult SendRescheduleEmail(string request)
        {
            _mailService.SendRescheduleEmail(request);
            return Ok();
        }
       
    }
}

