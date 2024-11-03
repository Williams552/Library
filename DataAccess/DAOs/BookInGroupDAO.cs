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
    public class BookInGroupDAO : SingletonBase<BookInGroupDAO>
    {
        public BookInGroupDAO() { }
        private readonly JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookInGroupDAO(JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
        {
            _jwtTokenService = jwtTokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        // Lay id cua tai khoan dang dang nhap dua tren jwt token lay duoc
        private int GetUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return 0;
        }

        // xem tat ca nhom sach hien co
        public async Task<IEnumerable<BookInGroup>> getAllBookInGroups()
        {
            return await _context.BookInGroups.Where(a => !a.IsDeleted).ToListAsync();
        }

        // lay nhom sach theo id
        public async Task<BookInGroup> getBookInGroupById(int id)
        {
            return await _context.BookInGroups.FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        // them nhom sach moi
        public async Task<(bool, string mess, BookInGroup)> createBookInGroup(BookInGroup bookInGroup)
        {
            bookInGroup.IsDeleted = false;
            bookInGroup.CreatedBy = GetUserId();
            bookInGroup.CreatedAt = DateTime.Now;
            await _context.BookInGroups.AddAsync(bookInGroup);
            await _context.SaveChangesAsync();
            return (true, "Thêm thành công", bookInGroup);
        }

        // Cap nhat lai nhom sach
        public async Task<(bool, string mess, BookInGroup)> updateBookGroup(BookInGroup bookInGroup)
        {
            var existingBookInGroup = await getBookInGroupById(bookInGroup.Id);
            if (existingBookInGroup == null)
            {
                return (false, "Không tìm thấy nhóm sách", null);
            }
            existingBookInGroup.UpdatedAt = DateTime.Now;
            existingBookInGroup.UpdatedBy = GetUserId();
            existingBookInGroup.IsDeleted = false;
            existingBookInGroup.BookId = bookInGroup.BookId;
            existingBookInGroup.GroupId = bookInGroup.GroupId;
            await _context.SaveChangesAsync();
            return (true, "Cập nhật thành công", existingBookInGroup);
        }

        // Xoa nhom sach
        public async Task<(bool, string mess)> deleteBookInGroup(int id)
        {
            var bookGroup = await getBookInGroupById(id);
            if (bookGroup == null)
            {
                return (false, "Không tìm thấy nhóm sách cần xóa.");
            }
            bookGroup.IsDeleted = true;
            bookGroup.DeletedBy = GetUserId();
            bookGroup.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return (true, "Xóa thành công.");
        }

    }
}
