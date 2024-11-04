﻿using Microsoft.AspNetCore.Mvc;
using Library_Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace Library_Web.Pages.Members
{
    public class RemoveFavoriteModel : PageModel
    {
        [BindProperty]
        public FavoritesList item { get; set; } = new FavoritesList();
        private readonly HttpClient _httpClient;
        private readonly ILogger<RemoveFavoriteModel> _logger;

        public RemoveFavoriteModel(HttpClient httpClient, ILogger<RemoveFavoriteModel> logger)
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
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            AttachJwtTokenToClient();
            var userId = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("User ID không có trong session.");
                return RedirectToPage("/Members/Login");
            }
            var response = await _httpClient.DeleteAsync($"http://localhost:5139/api/FavoriteBook/remove/{id}?userId={userId}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessAdd"] = "Xóa thành công";
                return RedirectToPage("/Index");
            }
            else
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Failed to delete favorite. Status Code: {response.StatusCode}, Details: {errorDetails}");
                TempData["ErrorMessage"] = "Xóa thất bại";
                return RedirectToPage("/Index");
            }
        }

    }
}