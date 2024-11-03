using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class MemberGroupDAO : SingletonBase<MemberGroupDAO>
    {
        public MemberGroupDAO() { }
        private readonly JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MemberGroupDAO(JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
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

        // Lấy tất cả
        public async Task<IEnumerable<MemberGroup>> getAllMemberGroup()
        {
            return await _context.MemberGroups.Where(a => !a.IsDeleted).ToListAsync();
        }

        // Lay nhomm thanh vien theo id
        public async Task<MemberGroup> getMemberGroupById(int id)
        {
            return await _context.MemberGroups.FirstOrDefaultAsync(a => a.GroupId == id && !a.IsDeleted);
        }

        // Them thanh vien moi
        public async Task<(bool success, string mess, MemberGroup)> createMemberGroup(MemberGroup member)
        {
            var existingNumber = await _context.MemberGroups.FirstOrDefaultAsync(a => a.GroupId == member.GroupId);
            if (existingNumber != null)
            {
                return (false, "ID này đã tồn tại trong hệ thống.", null);
            }
            member.IsDeleted = false;
            member.CreatedBy = GetUserId();
            member.CreatedAt = DateTime.Now;
            await _context.MemberGroups.AddAsync(member);
            await _context.SaveChangesAsync();
            return (true, "Thêm thành công.", member);
        }

        // Cap nhat nhom
        public async Task<(bool success, string mess, MemberGroup)> updateMemberGroup(MemberGroup member)
        {
            var existingMember = await getMemberGroupById(member.GroupId);
            if (existingMember == null)
            {
                return (false, "Không tìm thấy Nhóm này", null);
            }
            existingMember.IsDeleted = false;
            existingMember.UpdatedBy = GetUserId();
            existingMember.UpdatedAt = DateTime.Now;
            existingMember.UpdatedBy = member.UpdatedBy;
            await _context.SaveChangesAsync();
            return (true, "Member updated successfully.", existingMember);
        }

        // Xoa nhom
        public async Task<(bool success, string message)> DeleteMemberGroup(int id)
        {
            var member = await getMemberGroupById(id);
            if (member == null)
            {
                return (false, "Không tìm thấy nhóm.");
            }
            member.IsDeleted = true;
            member.DeletedBy = GetUserId();
            member.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return (true, "Xóa thành công.");
        }

    }
}
