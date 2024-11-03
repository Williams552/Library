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
    public class LoanDAO : SingletonBase<LoanDAO>
    {
        public LoanDAO() { }
        private readonly JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoanDAO(JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
        {
            _jwtTokenService = jwtTokenService;
            _httpContextAccessor = httpContextAccessor;
        }
        public int GetUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return 0;
        }

        public async Task<Loan> findById(int id)
        {
            return await _context.Loans.FirstOrDefaultAsync(a => a.LoanId == id && !a.IsDeleted);
        }

        public async Task<IEnumerable<Loan>> getAll()
        {
            return await _context.Loans.Where(a => !a.IsDeleted).ToListAsync();
        }

        public async Task<Loan> create(Loan loan)
        {
            loan.CreatedBy = GetUserId();
            loan.CreatedAt = DateTime.Now;
            loan.LoanDate = DateTime.Now;
            loan.IsDeleted = false;
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<Loan> update(Loan author)
        {
            var existingAuthor = await findById(author.LoanId);
            if (existingAuthor != null)
            {
                existingAuthor.UpdatedBy = GetUserId();
                existingAuthor.UpdatedAt = DateTime.Now;
                existingAuthor.UserId = author.UserId;
                existingAuthor.CopyId = author.CopyId;
                existingAuthor.DueDate = author.DueDate;
                existingAuthor.ReturnDate = author.ReturnDate;
                existingAuthor.BorrowFee = author.BorrowFee;
                existingAuthor.IsDeleted = false;
                existingAuthor.Status = author.Status;
                existingAuthor.Fine = author.Fine;
                await _context.SaveChangesAsync();
                return existingAuthor;
            }
            return null;
        }
    }
}
