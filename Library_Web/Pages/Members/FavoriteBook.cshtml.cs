using Library_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Library_Web.Pages.Members
{
    public class FavoriteBookModel : PageModel
    {
        [BindProperty]
        public IEnumerable<FavoritesList> item { get; set; }
        private readonly HttpClient _httpClient;
        private readonly ILogger<FavoriteBookModel> _logger;

        public FavoriteBookModel(HttpClient httpClient, ILogger<FavoriteBookModel> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        private void AttachJwtTokenToClient()
        {
            var token = HttpContext.Session.GetString("Token");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            RedirectToPage("/Members/Login");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            AttachJwtTokenToClient();
            var userId = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("User ID not found in session. Redirecting to login page.");
                return RedirectToPage("/Members/Login");
            }
            var response = await _httpClient.GetAsync($"http://localhost:5139/api/FavoriteBook?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                try
                {
                    item = JsonSerializer.Deserialize<IEnumerable<FavoritesList>>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? Enumerable.Empty<FavoritesList>();
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"JSON parsing error: {ex.Message}");
                    item = Enumerable.Empty<FavoritesList>();
                }
            }
            else
            {
                _logger.LogError("Cannot retrieve favorite books from API. Error code: " + response.StatusCode);
                item = Enumerable.Empty<FavoritesList>();
            }
            return Page();
        }
    }
}
