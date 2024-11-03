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
    public class BookDAO : SingletonBase<BookDAO>
    {
        public BookDAO() { }

        private readonly JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookDAO(JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
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

        // Lấy tất cả các sach hiện có 
        public async Task<IEnumerable<Book>> getAllBook()
        {
            return await _context.Books
        .Include(b => b.Author)
        .Include(b => b.Category) 
        .Include(b => b.Publisher)
        .Include(b => b.BookCopy)
        .Include(b => b.BookInGroups)
        .Include(b => b.FavoritesLists)
        .Include(b => b.LiquidatedBooks)
        .Include(b => b.ReadingProgresses)
        .Include(b => b.Reviews)
        .Where(b => b.IsDeleted == false)
        .ToListAsync();
        }

        // Tim sach theo id
        public async Task<Book> findBookById(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(a => a.BookId == id && a.IsDeleted == false);
            if (book != null)
            {
                // Tăng lượt xem của sách lên 1
                book.Views = (book.Views ?? 0) + 1;
                await _context.SaveChangesAsync();
            }
            return book;
        }

        // Tim sach theo ten
        public async Task<IEnumerable<Book>> findBook(string name)
        {
            return await _context.Books.Where(a => a.Title.Contains(name) && a.IsDeleted == false).ToListAsync();
        }

        // Thêm mới sach
        public async Task<Book> createBook(Book book)
        {
            book.CreatedBy = GetUserId();
            book.CreatedAt = DateTime.Now;
            book.IsDeleted = false;
            book.DamageFee = book.Price * 0.2m;
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        // Update sach
        public async Task<Book> updateBook(Book book)
        {
            var existingBook = await findBookById(book.BookId);
            if (existingBook != null)
            {
                existingBook.UpdatedBy = GetUserId();
                existingBook.UpdatedAt = DateTime.Now;
                existingBook.Title = book.Title;
                existingBook.Description = book.Description;
                existingBook.PublishYear = book.PublishYear;
                existingBook.MaxCopiesPerShelf = book.MaxCopiesPerShelf;
                existingBook.AuthorId = book.AuthorId;
                existingBook.CategoryId = book.CategoryId;
                existingBook.SupplierId = book.SupplierId;
                existingBook.PublisherId = book.PublisherId;
                existingBook.Price = book.Price;
                existingBook.AvailableCopies = book.AvailableCopies;
                existingBook.Warehouse = book.Warehouse;
                existingBook.DamageFee = book.Price * 0.2m;
                existingBook.Cover = book.Cover;
                existingBook.PdfLink = book.PdfLink;
                existingBook.IsDeleted = false;
                await _context.SaveChangesAsync();
                return existingBook;
            }
            return null;
        }

        // Xoa sach
        public async Task<bool> deleteBook(int id)
        {
            var book = await findBookById(id);
            if (book != null)
            {
                book.IsDeleted = true;
                book.DeletedBy = GetUserId();
                book.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
