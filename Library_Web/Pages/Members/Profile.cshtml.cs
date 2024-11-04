using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Net.Http;
using Library_Web.Models;
using System.Text.Json;
using System.Text;

namespace Library_Web.Pages.Members
{
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public Member item { get; set; }
        private readonly ILogger<ProfileModel> _logger;
        private readonly HttpClient _httpClient;

        public ProfileModel(HttpClient httpClient, ILogger<ProfileModel> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }   

        public async Task<IActionResult> OnGetAsync()
        {
            AttachJwtTokenToClient();
            var userId = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("User ID không có trong session.");
                return RedirectToPage("/Members/Login"); // Điều hướng tới trang đăng nhập nếu không có userId
            }
            var responseUrl = await _httpClient.GetAsync($"http://localhost:5139/api/Member/{userId}");
            if (responseUrl.IsSuccessStatusCode)
            {
                var responseContent = await responseUrl.Content.ReadAsStringAsync();
                //_logger.LogInformation("API Response Content: " + responseContent);
                try
                {
                    item = JsonSerializer.Deserialize<Member>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                catch (JsonException ex)
                {
                    item = null;
                }
            }
            else
            {
                _logger.LogError("Không thể lấy danh sách sách từ API. Mã lỗi: " + responseUrl.StatusCode);
                item = null;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            AttachJwtTokenToClient();
            var jsonContent = JsonSerializer.Serialize(item);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"http://localhost:5139/api/Member/{item.MemberId}", content);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Cập nhật thông tin tài khoản thành công.");
                TempData["SuccessMessage"] = "Thông tin tài khoản đã được cập nhật thành công!";
            }
            else
            {
                _logger.LogError("Cập nhật thông tin tài khoản thất bại. Mã lỗi: " + response.StatusCode);
                TempData["ErrorMessage"] = "Không thể cập nhật thông tin tài khoản. Vui lòng thử lại.";
            }

            // Trở lại trang hiện tại để hiển thị thông báo
            return RedirectToPage();
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
