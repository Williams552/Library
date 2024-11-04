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
    public class BookAccessForMemberGroupDAO : SingletonBase<BookAccessForMemberGroupDAO>
    {
        public BookAccessForMemberGroupDAO() { }
        private readonly JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookAccessForMemberGroupDAO(JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
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

        // lay tat ca
        public async Task<IEnumerable<BookAccessForMemberGroup>> getAllBookAccessForMemberGroup()
        {
            return await _context.BookAccessForMemberGroups.Where(a => !a.IsDeleted).ToListAsync();
        }

        // lay theo id
        public async Task<BookAccessForMemberGroup> getBookAccessForMemberGroupById(int id)
        {
            return await _context.BookAccessForMemberGroups.FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        // them moi
        public async Task<(bool, string mess, BookAccessForMemberGroup)> createBookAccessForMemberGroup(BookAccessForMemberGroup bookAccessForMemberGroup)
        {
            bookAccessForMemberGroup.IsDeleted = false;
            bookAccessForMemberGroup.CreatedBy = GetUserId();
            bookAccessForMemberGroup.CreatedAt = DateTime.Now;
            await _context.BookAccessForMemberGroups.AddAsync(bookAccessForMemberGroup);
            await _context.SaveChangesAsync();
            return (true, "Thêm thành công", bookAccessForMemberGroup);
        }

        // Cap nhat lai
        public async Task<(bool, string mess, BookAccessForMemberGroup)> updateBookAccessForMemberGroup(BookAccessForMemberGroup bookAccessForMemberGroup)
        {
            var existingBookGroup = await getBookAccessForMemberGroupById(bookAccessForMemberGroup.GroupId);
            if (existingBookGroup == null)
            {
                return (false, "Không tìm thấy nhóm sách", null);
            }
            existingBookGroup.UpdatedAt = DateTime.Now;
            existingBookGroup.UpdatedBy = GetUserId();
            existingBookGroup.IsDeleted = false;
            existingBookGroup.BookGroupId = bookAccessForMemberGroup.BookGroupId;
            existingBookGroup.GroupId = bookAccessForMemberGroup.GroupId;
            await _context.SaveChangesAsync();
            return (true, "Cập nhật thành công", existingBookGroup);
        }

        // Xoa nhom sach
        public async Task<(bool, string mess)> deleteBookAccessForMemberGroup(int id)
        {
            var bookGroup = await getBookAccessForMemberGroupById(id);
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
