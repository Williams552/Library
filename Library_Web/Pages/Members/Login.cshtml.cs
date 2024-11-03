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

namespace Library_Web.Pages.Members
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private class LoginResponse
        {
            public string message { get; set; }
            public string token { get; set; }
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

        public LoginModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

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
                if (claims.ContainsKey("fullName"))
                {
                    var fullName = claims["fullName"];
                    HttpContext.Session.SetString("fullName", fullName);
                }
                return RedirectToPage("/Index");
            }
            else
            {
                // Thông báo lỗi nếu đăng nhập thất bại
                ModelState.AddModelError(string.Empty, "Đăng nhập không thành công");
                Console.WriteLine($"Username: {Username}, Password: {Password}");
                return Page();
            }
        }

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
