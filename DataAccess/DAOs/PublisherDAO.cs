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
    public class PublisherDAO : SingletonBase<PublisherDAO>
    {
        public PublisherDAO() { }
        private readonly JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Constructor có tham số JwtTokenService
        public PublisherDAO(JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
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

        // Lay tat ca cac nha xuat ban
        public async Task<IEnumerable<Publisher>> getAllPublishers()
        {
            return await _context.Publishers.Where(a => !a.IsDeleted).ToListAsync();
        }

        // Lay thong nha xuat ban theo id
        public async Task<Publisher> getPublisherById(int id)
        {
            return await _context.Publishers.FirstOrDefaultAsync(a => a.PublisherId == id && !a.IsDeleted);
        }

        // them nha xuat ban moi
        public async Task<(bool, string mess, Publisher)> createPublisher(Publisher publisher)
        {
            publisher.CreatedBy = GetUserId();
            publisher.CreatedAt = DateTime.Now;
            publisher.IsDeleted = false;
            await _context.Publishers.AddAsync(publisher);
            await _context.SaveChangesAsync();
            return (true, "Thêm thành công.", publisher);
        }

        // cap nhat nha xuat ban
        public async Task<(bool, string mess, Publisher)> updatePublisher(Publisher publisher)
        {
            var existingPublisher = await getPublisherById(publisher.PublisherId);
            if (existingPublisher != null)
            {
                existingPublisher.UpdatedBy = GetUserId();
                existingPublisher.UpdatedAt = DateTime.Now;
                existingPublisher.Name = publisher.Name;
                existingPublisher.Address = publisher.Address;
                existingPublisher.IsDeleted = false;
                await _context.SaveChangesAsync();
                return (true, "Cập nhật thành công.", existingPublisher);
            }
            return (false, "Nhà xuất bản hông tồn tại.", null);
        }

        // Xoa nha xuat ban
        public async Task<(bool, string mess)> deletePublisher(int id)
        {
            var publisher = await getPublisherById(id);
            if (publisher != null)
            {
                publisher.IsDeleted = true;
                publisher.DeletedBy = GetUserId();
                publisher.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return (true, "Xóa thành công");
            }
            return (false, " Xóa thất bại");
        }
    }
}
