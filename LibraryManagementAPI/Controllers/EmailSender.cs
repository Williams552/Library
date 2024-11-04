using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailSenderController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        // API để gửi email
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail(string email, string subject, string message)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(message))
            {
                return BadRequest("Email, subject, và message không được để trống.");
            }

            try
            {
                await _emailSender.SendEmailAsync(email, subject, message);
                return Ok("Email đã được gửi thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi khi gửi email: {ex.Message}");
            }
        }
    }
}
