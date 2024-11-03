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

namespace DataAccess.DAOs
{
    public class CategoryDAO : SingletonBase<CategoryDAO>
    {
        public CategoryDAO() { }
        private readonly JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Constructor có tham số JwtTokenService
        public CategoryDAO(JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
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

        // lay moi loai sach
        public async Task<IEnumerable<Category>> getAllCategories()
        {
            return await _context.Categories.Where(a => !a.IsDeleted).ToListAsync();
        }

        // lay sach theo id 
        public async Task<Category> getCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(a => a.CategoryId == id && !a.IsDeleted);
        }

        // Them loai sach moi
        public async Task<(bool, string mess, Category)> addCategory(Category category)
        {
            var existingCategory = await _context.Categories.Where(c => c.Name == category.Name || c.CategoryCode == category.CategoryCode && !c.IsDeleted).FirstOrDefaultAsync();
            if (existingCategory != null)
            {
                return (false, "Thể loại này đã tồn tại.", null);
            }
            category.IsDeleted = false;
            category.CreatedBy = GetUserId();
            category.CreatedAt = DateTime.Now;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return (true, "Thêm thành công." , category);
        }

        // cap nhat loai sach 
        public async Task<(bool, string mess, Category)> updateCategory(Category category)
        {
            var existingCategory = await getCategoryById(category.CategoryId);
            if (existingCategory != null)
            {
                existingCategory.IsDeleted = false;
                existingCategory.UpdatedBy = GetUserId();
                existingCategory.UpdatedAt = DateTime.Now;
                existingCategory.CategoryCode = category.CategoryCode;
                existingCategory.Name = category.Name;
                existingCategory.Description = category.Description;
                await _context.SaveChangesAsync();
                return (true, "Cập nhật thành công.", existingCategory);
            }
            return (false, "Thể loại này không tồn tại", null);
        }

        // Xoa loai sach
        public async Task<(bool, string mess)> deleteCategory(int id)
        {
            var existingCategory = await getCategoryById(id);
            if (existingCategory != null)
            {
                existingCategory.IsDeleted = true;
                existingCategory.DeletedBy = GetUserId();
                existingCategory.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return (true, "Xóa thành công");
            }
            return (false, "Không tìm thấy thể loại cần xóa");
        }
    }
}
