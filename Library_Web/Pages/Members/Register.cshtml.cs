using Library_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Library_Web.Models;
using Azure;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.SignalR.Hosting;
using System.Net.Http.Headers;

namespace Library_Web.Pages.Members
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public Member member { get; set; } = new Member();
        private readonly HttpClient _httpClient;
        private readonly ILogger<RegisterModel> _logger;
        public RegisterModel(HttpClient httpClient, ILogger<RegisterModel> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            AttachJwtTokenToClient();
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5139/api/Member", member);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessRegister"] = "Đăng kí thành công!";
                return RedirectToPage("/Members/Login");
            }
            else
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = "Username đã tồn tại";
                return RedirectToPage("/Members/Register");
            }
        }

        private void AttachJwtTokenToClient()
        {
            var token = HttpContext.Session.GetString("Token");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
