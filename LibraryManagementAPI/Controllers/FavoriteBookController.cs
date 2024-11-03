using DataAccess.DAOs;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using Repository.interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteBookController : Controller
    {
        private readonly FavoritesListDAO _favoritesListDao;
        public FavoriteBookController(FavoritesListDAO favoritesListDao)
        {
            _favoritesListDao = favoritesListDao;
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetTotalLikes(int bookId)
        {
            var totalLikes = await _favoritesListDao.GetTotalLikesAsync(bookId);
            return Ok(new { totalLikes });
        }

        [HttpGet]
        public async Task<IActionResult> GetUserFavorites()
        {
            var favorites = await _favoritesListDao.GetFavoritesByUserIdAsync();
            return Ok(favorites);
        }

        [HttpPost("addFavorite")]
        public async Task<IActionResult> AddFavorite([FromBody] int bookId)
        {
            var result = await _favoritesListDao.AddFavoriteAsync(bookId);
            if (result)
            {
                return Ok(new { message = "Book added to favorites successfully." });
            }
            return BadRequest(new { message = "Book is already in favorites or user not authenticated." });
        }

        [HttpPost("removeFavorite")]
        public async Task<IActionResult> RemoveFavorite([FromBody] int bookId)
        {
            var result = await _favoritesListDao.RemoveFavoriteAsync(bookId);
            if (result)
            {
                return Ok(new { message = "Book removed from favorites successfully." });
            }
            return BadRequest(new { message = "Failed to remove book from favorites or book was not in favorites." });
        }

    }
}
