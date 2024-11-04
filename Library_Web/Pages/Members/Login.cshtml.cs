using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;
using Library_Web.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Library_Web.Pages.Books;

namespace Library_Web.Pages.Members
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LoginModel> _logger;

        private class LoginResponse
        {
            public string message { get; set; }
            public string token { get; set; }
        }

        // Giữ lại constructor này
        public LoginModel(HttpClient httpClient, ILogger<LoginModel> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        private IDictionary<string, string> DecodeJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);
        }

        [BindProperty]
        public string FullName { get; set; } = null!;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        private void AttachJwtTokenToClient()
        {
            var token = HttpContext.Session.GetString("Token");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var (loginSuccess, token) = await CheckLoginAsync(Username, Password);
            if (loginSuccess)
            {
                var claims = DecodeJwtToken(token);
                HttpContext.Session.SetString("Token", token);
                if (claims.ContainsKey("fullName") && claims.ContainsKey("userID"))
                {
                    var fullName = claims["fullName"];
                    HttpContext.Session.SetString("fullName", fullName);
                    var id = claims["userID"];
                    HttpContext.Session.SetString("userId", id);
                    //_logger.LogInformation("User ID được lưu trong session: " + id);
                    return RedirectToPage("/Index");
                }
                else
                {
                    _logger.LogError("Không tìm thấy userID trong token claims.");
                    return RedirectToPage("/Index");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Đăng nhập không thành công");
                Console.WriteLine($"Username: {Username}, Password: {Password}");
                return Page();
            }
        }

        //public async Task<IActionResult> OnGetAsync()
        //{
        //    // Kiểm tra xem người dùng đã đăng nhập hay chưa
        //    var token = HttpContext.Session.GetString("Token");
        //    var userId = HttpContext.Session.GetString("userId");

        //    if (!string.IsNullOrEmpty(token) || !string.IsNullOrEmpty(userId))
        //    {
        //        // Nếu người dùng đã đăng nhập, xóa các session hiện tại
        //        HttpContext.Session.Remove("Token");
        //        HttpContext.Session.Remove("userId");
        //        HttpContext.Session.Remove("fullName"); // Xóa thêm các session khác nếu cần
        //        _logger.LogInformation("Session đã được xóa vì người dùng quay lại trang đăng nhập.");
        //    }

        //    return Page();
        //}

        private async Task<(bool loginSuccess, string token)> CheckLoginAsync(string username, string password)
        {
            AttachJwtTokenToClient();
            var requestUri = $"http://localhost:5139/api/Member/checkLogin?username={username}&password={password}";
            var response = await _httpClient.PostAsync(requestUri, null);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Đăng nhập thất bại. StatusCode: " + response.StatusCode);
                return (false, null);
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<LoginResponse>(responseContent);

            return (true, responseData?.token);
        }
    }

}
