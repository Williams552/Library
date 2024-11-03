using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.DAOs
{
    public class AuthorDAO : SingletonBase<AuthorDAO>
    {
        public AuthorDAO() { }

        private readonly JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Constructor có tham số JwtTokenService
        public AuthorDAO(JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
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

        // Lấy tất cả các tác giả hiện có 
        public async Task<IEnumerable<Author>> getAllAuthor()
        {
            return await _context.Authors.Where(a => !a.IsDeleted).ToListAsync();
        }

        // Thêm mới tác gia
        public async Task<Author> createAuthor(Author author)
        {
            author.CreatedBy = GetUserId();
            author.CreatedAt = DateTime.Now;
            author.IsDeleted = false;
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }

        // Update tac gia
        public async Task<Author> updateAuthor(Author author)
        {
            var existingAuthor = await findAuthorById(author.AuthorId);
            if (existingAuthor != null)
            {
                existingAuthor.UpdatedBy = GetUserId();
                existingAuthor.UpdatedAt = DateTime.Now;
                existingAuthor.FullName = author.FullName;
                existingAuthor.Biography = author.Biography;
                existingAuthor.Avartar = author.Avartar;
                existingAuthor.IsDeleted = false;
                await _context.SaveChangesAsync();
                return existingAuthor;
            }
            return null;
        }

        // Tim tac gia theo ten
        public async Task<IEnumerable<Author>> findAuthor(string name)
        {
            return await _context.Authors.Where(a => a.FullName.Contains(name) && !a.IsDeleted).ToListAsync();
        }

        // Tim tac gia theo id
        public async Task<Author> findAuthorById(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == id && !a.IsDeleted);
        }

        // Xoa tac gia
        public async Task<bool> deleteAuthor(int id)
        {
            var author = await findAuthorById(id);
            if (author != null)
            {
                author.IsDeleted = true;
                author.DeletedBy = GetUserId();
                author.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
