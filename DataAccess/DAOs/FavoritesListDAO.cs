using System;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Net;

namespace DataAccess.DAOs
{
    public class FavoritesListDAO : SingletonBase<FavoritesListDAO>
    {
        public FavoritesListDAO() { }
        private readonly JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FavoritesListDAO(JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
        {
            _jwtTokenService = jwtTokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return 0;
        }

        // lay tong luot thich
        public async Task<int> GetTotalLikesAsync(int bookId)
        {
            // Đếm số lượng người dùng yêu thích cuốn sách dựa vào bookId
            return await _context.FavoritesLists
                .CountAsync(f => f.BookId == bookId && !f.IsDeleted);
        }

        // lay sach yeu thich theo id 
        public async Task<IEnumerable<FavoritesList>> GetFavoritesByUserIdAsync()
        {
            return await _context.FavoritesLists
                .Where(f => f.UserId == GetUserId() && !f.IsDeleted)
                .Include(f => f.Book) // Bao gồm thông tin sách
                .ToListAsync();
        }

        // Thêm sách vào danh sách yêu thích của người dùng
        public async Task<bool> AddFavoriteAsync(int bookId)
        {
            int userId = GetUserId();
            var existingFavorite = await _context.FavoritesLists
                .FirstOrDefaultAsync(f => f.UserId == userId && f.BookId == bookId);
            if (existingFavorite == null)
            {
                var favorite = new FavoritesList
                {
                    UserId = userId,
                    BookId = bookId,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                };

                _context.FavoritesLists.Add(favorite);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveFavoriteAsync(int bookId)
        {
            int userId = GetUserId();
            var existingFavorite = await _context.FavoritesLists
                .FirstOrDefaultAsync(f => f.UserId == userId && f.BookId == bookId);

            if (existingFavorite != null)
            {
                _context.FavoritesLists.Remove(existingFavorite);
                await _context.SaveChangesAsync();
                return true;
            }
            return false; 
        }
    }
}
