using System;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DataAccess.DAOs
{
    public class FeeDAO : SingletonBase<FeeDAO>
    {
        public FeeDAO() { }

        private readonly JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Constructor có tham số JwtTokenService
        public FeeDAO(JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
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

        // Lay tat ca loai phi
        public async Task<IEnumerable<Fee>> getAllFees()
        {
            return await _context.Fees.Where(a => !a.IsDeleted).ToListAsync();
        }

        // Lay loai phi theo id
        public async Task<Fee> getFeeById(int id)
        {
            return await _context.Fees.FirstOrDefaultAsync(a => !a.IsDeleted && a.FeeId == id);
        }

        // tao phi moi
        public async Task<(bool, string mess ,Fee)> createFee(Fee fee)
        {
            fee.IsDeleted = false;
            fee.CreatedBy = GetUserId();
            fee.CreatedAt = DateTime.UtcNow;
            await _context.Fees.AddAsync(fee);
            await _context.SaveChangesAsync();
            return (true, "Thêm thành công",fee);
        }

        // cap nhat phi
        public async Task<(bool, string mess, Fee)> updateFee(Fee fee)
        {
            var existingFees = await getFeeById(fee.FeeId);
            if (existingFees == null)
            {
                return (false, "Không tìm thấy loại phí cần cập nhật", null);
            }
            existingFees.IsDeleted = false;
            existingFees.UpdatedBy = GetUserId();
            existingFees.UpdatedAt = DateTime.UtcNow;
            existingFees.Name = fee.Name;
            existingFees.FeeType = fee.FeeType;
            existingFees.Amount = fee.Amount;
            existingFees.Description = fee.Description;
            existingFees.MinPrice = fee.MinPrice;
            existingFees.MaxPrice = fee.MaxPrice;
            await _context.SaveChangesAsync();
            return(true, "Cập nhật thành công", existingFees);
        }

        // Xoa phi
        public async Task<(bool, string mess)> deleteFee(int id)
        {
            var existingFees = await getFeeById(id);
            if (existingFees == null)
            {
                return (false, "Không tìm thấy dữ liệu cần xóa");
            }
            existingFees.IsDeleted = true;
            existingFees.DeletedBy = GetUserId();
            existingFees.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return (true, "Xóa thành công");
        }

        // Tinh so phi
        //public async Task<Fee> calculateFee(Fee fee)
        //{
        //    return 
        //}

     }
}
