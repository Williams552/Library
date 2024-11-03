using Library_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library_Web.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IEnumerable<Book> book { get; set; }
        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient, ILogger<IndexModel> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        //public async Task<IActionResult> SearchByCategory(int categoryId)
        //{
        //    var books = await _context.Books
        //        .Include(b => b.Author)
        //        .Where(b => b.CategoryId == categoryId && !b.IsDeleted)
        //        .ToListAsync();

        //    return Page(); // Trả về view với danh sách sách
        //}

        public async Task<IActionResult> OnGetAsync()
        {
            AttachJwtTokenToClient();
            var response = await _httpClient.GetAsync("http://localhost:5139/api/Book");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                //_logger.LogInformation("API Response Content: " + responseContent);

                try
                {
                    book = JsonSerializer.Deserialize<IEnumerable<Book>>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                catch (JsonException ex)
                {
                    book = Enumerable.Empty <Book>();
                }
            }
            else
            {
                _logger.LogError("Không thể lấy danh sách sách từ API. Mã lỗi: " + response.StatusCode);
                book = Enumerable.Empty<Book>();
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
