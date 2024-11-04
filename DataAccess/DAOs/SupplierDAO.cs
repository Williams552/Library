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
    public class SupplierDAO : SingletonBase<SupplierDAO>
    {
        public SupplierDAO() { }
        private readonly JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Constructor có tham số JwtTokenService
        public SupplierDAO(JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
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

        // hien tat ca nha cung cap 
        public async Task<IEnumerable<Supplier>> getAllSuppliers()
        {
           return await _context.Suppliers.Where(a=> !a.IsDeleted).ToListAsync();
        }

        // lay nha cung cap theo id
        public async Task<Supplier> getSupplierById(int id)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(a => a.SupplierId == id && !a.IsDeleted);
        }

        // Them nha cung cap
        public async Task<(bool, string mess, Supplier)> createSupplier(Supplier supplier)
        {
            supplier.CreatedBy = GetUserId();
            supplier.CreatedAt = DateTime.Now;
            supplier.IsDeleted = false;
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
            return (true, "Thêm thành công", supplier);
        }

        // Cap nhat nha cung cap
        public async Task<(bool, string mess, Supplier)> updateSupplier(Supplier supplier)
        {
            var existingSuplier = await getSupplierById(supplier.SupplierId);
            if (existingSuplier != supplier)
            {
                existingSuplier.IsDeleted = false;
                existingSuplier.UpdatedBy = GetUserId();
                existingSuplier.UpdatedAt = DateTime.Now;
                existingSuplier.Name = supplier.Name;
                existingSuplier.ContactInfo = supplier.ContactInfo;
                await _context.SaveChangesAsync();
                return (true, "Cập nhật thành công", existingSuplier);
            }
            return (false, "Cập nhật thất bại", null);
        }

        // Xoa nha cung cap
        public async Task<(bool, string mess)> deleteSupplier(int id)
        {
            var Suplier = await getSupplierById(id);
            if (Suplier != null)
            {
                Suplier.IsDeleted = true;
                Suplier.DeletedBy = GetUserId();
                Suplier.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return (true, "Xóa thành công");
            }
            return (false, "Xóa thất bại");
        }
    }
}
