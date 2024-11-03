using System;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DataAccess.DAOs
{
    public class BookGroupDAO : SingletonBase<BookGroupDAO>
    {
        public BookGroupDAO() { }
        private readonly JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookGroupDAO(JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
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

        // Lay ten cua tai khoan dang dang nhap dua tren jwt token lay duoc
        private string GetUserName()
        {
            var fullName = _httpContextAccessor.HttpContext.User.Identity.Name;
            return fullName;
        }

        // xem tat ca nhom sach hien co
        public async Task<IEnumerable<BookGroup>> getAllBookGroups()
        {
            return await _context.BookGroups.Where(a => !a.IsDeleted).ToListAsync();
        }

        // lay nhom sach theo id
        public async Task<BookGroup> getBookGroupById(int  id)
        {
            return await _context.BookGroups.FirstOrDefaultAsync(a => a.GroupId == id && !a.IsDeleted);
        }

        // them nhom sach moi
        public async Task<(bool, string mess, BookGroup)> createBookGroup(BookGroup bookGroup)
        {
            bookGroup.IsDeleted = false;
            bookGroup.CreatedBy = GetUserId();
            bookGroup.CreatedAt = DateTime.Now;
            await _context.BookGroups.AddAsync(bookGroup);
            await _context.SaveChangesAsync();
            return (true,"Thêm thành công",bookGroup);
        }

        // Cap nhat lai nhom sach
        public async Task<(bool, string mess, BookGroup)> updateBookGroup(BookGroup bookGroup)
        {
            var existingBookGroup = await getBookGroupById(bookGroup.GroupId);
            if (existingBookGroup == null)
            {
                return (false, "Không tìm thấy nhóm sách", null);
            }
            existingBookGroup.UpdatedAt = DateTime.Now;
            existingBookGroup.UpdatedBy = GetUserId();
            existingBookGroup.IsDeleted = false;
            existingBookGroup.Name = bookGroup.Name;
            existingBookGroup.Description = bookGroup.Description;
            await _context.SaveChangesAsync();
            return (true, "Cập nhật thành công", existingBookGroup);
        }

        // Xoa nhom sach
        public async Task<(bool, string mess)> deleteBookGroup(int id)
        {
            var bookGroup = await getBookGroupById(id);
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
