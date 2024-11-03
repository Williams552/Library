using Library_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Library_Web.Pages.Books
{
    public class DetailModel : PageModel
    {
        [BindProperty]
        public Book book { get; set; }
        private readonly ILogger<DetailModel> _logger;
        private readonly HttpClient _httpClient;

        public DetailModel(HttpClient httpClient, ILogger<DetailModel> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            AttachJwtTokenToClient();
            var response = await _httpClient.GetAsync($"http://localhost:5139/api/Book/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                //_logger.LogInformation("API Response Content: " + responseContent);

                try
                {
                    // Giải mã JSON cho một đối tượng `Book` duy nhất
                    book = JsonSerializer.Deserialize<Book>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                catch (JsonException ex)
                {
                    book = null;
                }
            }
            else
            {
                _logger.LogError("Không thể lấy danh sách sách từ API. Mã lỗi: " + response.StatusCode);
                book = null;
            }

            return Page();
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
