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
        public async Task<IActionResult> GetUserFavorites(int userId)
        {
            if (userId == 0)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var favorites = await _favoritesListDao.GetFavoritesByUserIdAsync(userId);
            return Ok(favorites);
        }

        [HttpPost("add/{bookId}")]
        public async Task<IActionResult> AddFavorite(int bookId, int userId)
        {
            //int userId = GetUserId();
            var result = await _favoritesListDao.AddFavoriteAsync(bookId, userId);
            if (result)
            {
                return Ok(new { message = "Book added to favorites successfully." });
            }
            return BadRequest(new { message = "Book is already in favorites or does not exist." });
        }


        [HttpDelete("remove/{bookId}")]
        public async Task<IActionResult> RemoveFavorite(int bookId, int userId)
        {
            var result = await _favoritesListDao.RemoveFavoriteAsync(bookId, userId);
            if (result)
            {
                return Ok(new { message = "Book removed from favorites successfully." });
            }
            return BadRequest(new { message = "Failed to remove book from favorites or book was not in favorites." });
        }

        private int GetUserId()
        {
            var userIdClaim = User.FindFirst("userID");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return 0;
        }

    }
}
