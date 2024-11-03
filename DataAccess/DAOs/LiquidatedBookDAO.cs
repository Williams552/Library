using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class LiquidatedBookDAO : SingletonBase<LiquidatedBookDAO>
    {
        public LiquidatedBookDAO() { }
        private readonly JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LiquidatedBookDAO(JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
        {
            _jwtTokenService = jwtTokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        // Lay id cua tai khoan dang dang nhap dua tren jwt token lay duoc
        public int GetUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return 0;
        }

        public async Task<IEnumerable<LiquidatedBook>> getAllLiquidatedBook()
        {
            return await _context.LiquidatedBooks.Where(a => !a.IsDeleted).ToListAsync();
        }

        public async Task<LiquidatedBook> findLiquidatedBookById(int id)
        {
            return await _context.LiquidatedBooks.FirstOrDefaultAsync(a => a.LiquidatedId == id && !a.IsDeleted);
        }

        public async Task<(bool, string mess, LiquidatedBook)> createLiquidatedBook(LiquidatedBook liquidatedBook)
        {
            liquidatedBook.LiquidatedBy = GetUserId();
            liquidatedBook.LiquidatedDate = DateTime.Now;
            liquidatedBook.IsDeleted = false;
            await _context.LiquidatedBooks.AddAsync(liquidatedBook);
            await _context.SaveChangesAsync();
            return (true, "Thêm thành công", liquidatedBook);
        }

        public async Task<(bool, string mess)> deleteLiquidatedBook(int id)
        {
            var liquidatedBook = await findLiquidatedBookById(id);
            if (liquidatedBook != null)
            {
                liquidatedBook.IsDeleted = true;
                liquidatedBook.DeletedBy = GetUserId();
                liquidatedBook.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return (true, "Xóa thành công");
            }
            return (false, "Xóa thất bại");
        }
    }
}
