using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using Library_Web.Models;

public class IndexModel : PageModel
{
    [BindProperty]
    public IEnumerable<Book> book { get; set; }
    private readonly ILogger<IndexModel> _logger;
    private readonly HttpClient _httpClient;

    public List<int> FavoriteBookIds { get; set; } = new List<int>();

    public IndexModel(HttpClient httpClient, ILogger<IndexModel> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        AttachJwtTokenToClient();
        await LoadBooksAsync();
        await LoadFavoriteBooksAsync();
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

    private async Task LoadBooksAsync()
    {
        var response = await _httpClient.GetAsync("http://localhost:5139/api/Book");

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            try
            {
                book = JsonSerializer.Deserialize<IEnumerable<Book>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (JsonException ex)
            {
                book = Enumerable.Empty<Book>();
            }
        }
        else
        {
            _logger.LogError("Cannot retrieve books from API. Error code: " + response.StatusCode);
            book = Enumerable.Empty<Book>();
        }
    }

    private async Task LoadFavoriteBooksAsync()
    {
        var userId = HttpContext.Session.GetString("userId");
        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("User ID not found in session.");
            return;
        }

        var response = await _httpClient.GetAsync($"http://localhost:5139/api/FavoriteBook?userId={userId}");
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            try
            {
                var favoriteBooks = JsonSerializer.Deserialize<IEnumerable<FavoritesList>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                FavoriteBookIds = favoriteBooks.Select(f => f.BookId).ToList();
            }
            catch (JsonException ex)
            {
                _logger.LogError($"JSON parsing error: {ex.Message}");
                FavoriteBookIds = new List<int>();
            }
        }
        else
        {
            _logger.LogError("Cannot retrieve favorite books from API. Error code: " + response.StatusCode);
        }
    }
}
