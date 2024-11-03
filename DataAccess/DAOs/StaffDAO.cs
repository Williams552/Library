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
    public class StaffDAO : SingletonBase<StaffDAO>
    {
        public StaffDAO()
        {
        }
        private readonly JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Constructor có tham số JwtTokenService
        public StaffDAO(JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
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

        // Lay ten cua tai khoan dang dang nhap dua tren jwt token lay duoc
        public string GetUserName()
        {
            var fullName = _httpContextAccessor.HttpContext.User.Identity.Name;
            return fullName;
        }

        // Xem tat ca thanh vien
        public async Task<IEnumerable<Staff>> getAllStaff()
        {
            return await _context.Staff.Where(a => !a.IsDeleted).ToListAsync();
        }

        // Lay thanh vien theo id
        public async Task<Staff> getStaffById(int id)
        {
            return await _context.Staff.FirstOrDefaultAsync(a => a.StaffId == id && !a.IsDeleted);
        }

        // Them thanh vien moi
        public async Task<(bool success, string mess, Staff)> createStaff(Staff staff)
        {
            var existingNumber = await _context.Staff.FirstOrDefaultAsync(a => a.Username == staff.Username);
            if (existingNumber != null)
            {
                return (false, "Username này đã tồn tại trong hệ thống.", null);
            }
            staff.IsDeleted = false;
            staff.Role = "Staff";
            staff.CreatedBy = GetUserId();
            staff.CreatedAt = DateTime.Now;
            staff.Password = BCrypt.Net.BCrypt.HashPassword(staff.Password);
            await _context.Staff.AddAsync(staff);
            await _context.SaveChangesAsync();
            return (true, "Đăng kí   thành công.", staff);
        }

        // Cap nhat thanh vien 
        public async Task<(bool success, string mess, Staff)> updateStaff(Staff staff)
        {
            var existingMember = await getStaffById(staff.StaffId);
            if (existingMember == null)
            {
                return (false, "Không tìm thấy người này", null);
            }
            existingMember.IsDeleted = false;
            existingMember.UpdatedBy = GetUserId();
            existingMember.UpdatedAt = DateTime.Now;
            existingMember.UpdatedBy = staff.UpdatedBy;
            existingMember.FullName = staff.FullName;
            existingMember.Email = staff.Email;
            existingMember.Username = staff.Username;
            existingMember.Password = BCrypt.Net.BCrypt.HashPassword(staff.Password);
            existingMember.Role = staff.Role;
            await _context.SaveChangesAsync();
            return (true, "Member updated successfully.", existingMember);
        }

        // Xoa thanh vien
        public async Task<(bool success, string message)> DeleteStaff(int id)
        {
            var member = await getStaffById(id);
            if (member == null)
            {
                return (false, "Không tìm thấy thành viên");
            }
            member.IsDeleted = true;
            member.DeletedBy = GetUserId();
            member.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return (true, "Xóa thành công.");
        }

        public async Task<(bool success, string token, string message)> Login(string username, string password)
        {
            try
            {
                var staff = await _context.Staff.FirstOrDefaultAsync(a => a.Username == username && !a.IsDeleted);
                if (staff == null)
                {
                    return (false, null, "Tài khoản hoặc mật khẩu không chính xác.");
                }

                if (!BCrypt.Net.BCrypt.Verify(password, staff.Password))
                {
                    return (false, null, "Tài khoản hoặc mật khẩu không chính xác.");
                }
                var token = _jwtTokenService.GenerateJwtToken(staff);
                return (true, token, "Đăng nhập thành công.");
            }
            catch (Exception ex)
            {
                return (false, null, "Có lỗi xảy ra trong quá trình đăng nhập");
            }
        }
    }
}