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
    public class BookshelfDAO : SingletonBase<BookshelfDAO>
    {
        public BookshelfDAO() { }
        private readonly JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Constructor có tham số JwtTokenService
        public BookshelfDAO(JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
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

        // lay tat ca cac ke sach
        public async Task<IEnumerable<Bookshelf>> getAllBookshelves()
        {
            return await _context.Bookshelves.Where(a => !a.IsDeleted).ToListAsync();
        }

        // lay ke sach theo id
        public async Task<Bookshelf> getBookshelfById(int id)
        {
            return await _context.Bookshelves.FirstOrDefaultAsync(a => a.ShelfId == id && !a.IsDeleted);
        }

        // lay ke sach theo so ke
        public async Task<Bookshelf> getBookshelvesByShelfNumber(int id)
        {
            return await _context.Bookshelves.FirstOrDefaultAsync(a => a.ShelfNumber == id && !a.IsDeleted);
        }

        // Them ke sach moi
        public async Task<(bool, string mess, Bookshelf)> createBookshelves(Bookshelf bookshelf)
        {
            bookshelf.IsDeleted = false;
            bookshelf.CreatedBy = GetUserId();
            bookshelf.CreatedAt = DateTime.Now;
            await _context.Bookshelves.AddAsync(bookshelf);
            await _context.SaveChangesAsync();
            return (true, "Thêm thành công.", bookshelf);
        }

        // Cap nhat lai ke sach
        public async Task<(bool, string mess, Bookshelf)> updateBookshelf(Bookshelf bookshelf)
        {
            var exsitingBookShelf = await getBookshelfById(bookshelf.ShelfId);
            if (exsitingBookShelf == null)
            {
                return (false, "Không tìm thấy kệ sách.", null);
            }
            exsitingBookShelf.IsDeleted = false;
            exsitingBookShelf.UpdatedBy = GetUserId();
            exsitingBookShelf.UpdatedAt = DateTime.Now;
            exsitingBookShelf.ShelfNumber = bookshelf.ShelfNumber;
            exsitingBookShelf.RowNumber = bookshelf.RowNumber;
            exsitingBookShelf.ColumnNumber = bookshelf.ColumnNumber;
            await _context.SaveChangesAsync();
            return (true, "Cập nhật thành công.",  exsitingBookShelf);
        }

        // Xoa ke sach theo so ke
        public async Task<(bool, string mess)> deleteBookshelfByShelfNumber(int id)
        {
            var shelfNumber = await getBookshelvesByShelfNumber(id);
            if (shelfNumber == null)
            {
                return (false, "Không tìm thấy kệ sách.");
            }
            shelfNumber.IsDeleted = true;
            shelfNumber.DeletedBy = GetUserId();
            shelfNumber.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return (true, "Xoá thàng công");
        }
    }
}
